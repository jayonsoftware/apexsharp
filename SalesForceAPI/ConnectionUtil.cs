﻿using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using SalesForceAPI.Model.RestApi;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Serilog;

namespace SalesForceAPI
{
    public class ConnectionUtil
    {
        public static ApexSharpConfig Session { get; set; }

        public static ApexSharpConfig GetSession()
        {
            if (Session == null)
            {
                throw new SalesForceNoFileFoundException("Cofnig is Null");
            }
            if (Session.SessionCreationDateTime <= DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                Log.ForContext<ConnectionUtil>().Information("Session Expired, Creating a New Session");

                Session = CreateSession(Session);
            }
            return Session;
        }



        public static ApexSharpConfig LoadSession(string configFileLocation)
        {
            FileInfo loadFileInfo = new FileInfo(configFileLocation);
            if (loadFileInfo.Exists)
            {
                string json = File.ReadAllText(loadFileInfo.FullName);
                Session = JsonConvert.DeserializeObject<ApexSharpConfig>(json);

                if (Session.SessionCreationDateTime <= DateTimeOffset.Now.ToUnixTimeSeconds())
                {
                    Log.ForContext<ConnectionUtil>().Information("Session Expired, Creating a New Session");
                    Session = CreateSession(Session);
                    return Session;
                }
                else
                {
                    Log.ForContext<ConnectionUtil>().Information("Session info found in file {configFileLocation}", configFileLocation);
                    return Session;
                }
            }
            else
            {
                throw new SalesForceNoFileFoundException(loadFileInfo.FullName);
            }
        }

        public static ApexSharpConfig CreateSession(ApexSharpConfig config)
        {
            config.SalesForceUrl = config.SalesForceUrl + "services/Soap/c/" + config.SalesForceApiVersion + ".0/";
            config = GetNewConnection(config);

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(config.ConfigLocation.FullName));
            File.WriteAllText(config.ConfigLocation.FullName, json);

            return config;
        }

        private static ApexSharpConfig GetNewConnection(ApexSharpConfig config)
        {
            var xml = @"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:enterprise.soap.sforce.com"">
                    <soapenv:Header>
                        <urn:LoginScopeHeader>
                        <urn:organizationId></urn:organizationId>
                        <urn:portalId></urn:portalId>
                        </urn:LoginScopeHeader>
                    </soapenv:Header>
                    <soapenv:Body>
                        <urn:login>
                            <urn:username>" + config.SalesForceUserId + "</urn:username>" +
                            "<urn:password>" + config.SalesForcePassword + config.SalesForcePasswordToken + "</urn:password>" +
                        "</urn:login>" +
                    "</soapenv:Body>" +
                "</soapenv:Envelope>";


            var retrunXml = PostLoginTask(config.SalesForceUrl, xml);

            if (retrunXml.Contains("INVALID_LOGIN"))
            {
                throw new SalesForceInvalidLoginException("Invalid Login");
            }
            Envelope envelope = UtilXml.DeSerilizeFromXML<Envelope>(retrunXml);

            var soapIndex = envelope.Body.loginResponse.result.serverUrl.IndexOf(@"/Soap", StringComparison.Ordinal);
            var restUrl = envelope.Body.loginResponse.result.serverUrl.Substring(0, soapIndex);
            var restSessionId = "Bearer " + envelope.Body.loginResponse.result.sessionId;


            config.SalesForceUrl = envelope.Body.loginResponse.result.serverUrl;
            config.SessionId = envelope.Body.loginResponse.result.sessionId;
            config.RestUrl = restUrl;
            config.RestSessionId = restSessionId;
            config.SessionCreationDateTime = DateTimeOffset.Now.ToUnixTimeSeconds() + envelope.Body.loginResponse.result.userInfo.sessionSecondsValid;

            return config;
        }

        private static string PostLoginTask(string url, string json)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = new StringContent(json, Encoding.UTF8, "text/xml")
            };
            request.Headers.Add("SOAPAction", "''");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = httpClient.SendAsync(request).Result;

            string xml = responseMessage.Content.ReadAsStringAsync().Result;
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    Log.ForContext<ConnectionUtil>().Information(xml, "Login Success");
                    return xml;
                default:
                    Log.ForContext<ConnectionUtil>().Error(xml, "Login Fail");
                    return xml;
            }
        }
    }
}