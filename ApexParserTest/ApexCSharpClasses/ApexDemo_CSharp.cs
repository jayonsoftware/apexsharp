﻿namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;

    public class Demo
    {
        public Contact contact { get; set; }

        public Demo()
        {
            contact = new Contact();
        }

        public PageReference Save()
        {
            try
            {
                Soql.insert(contact);
            }
            catch (DMLException e)
            {
                ApexPages.AddMessages(e);
            }

            return null;
        }

        public static string UpdatePhone(string email, string newPhone)
        {
            List<Contact> contacts = GetContactByEMail(email);
            if (contacts.IsEmpty())
            {
                return "Not Found";
            }
            else
            {
                contacts[0].Phone = newPhone;
                UpdateContacts(contacts);
                return "Phone Number Updated";
            }
        }

        private static List<Contact> GetContactByEMail(string email)
        {
            List<Contact> contacts = Soql.query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = :email", email);
            return contacts;
        }

        public static List<Contact> GetContacts()
        {
            List<Contact> contacts = Soql.query<Contact>("SELECT Id, Email, Phone FROM Contact");
            return contacts;
        }

        private static void UpdateContacts(List<Contact> contacts)
        {
            Soql.update(contacts);
        }
    }
}
