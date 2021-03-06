﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser;
using ApexParser.Visitors;
using NUnit.Framework;

namespace ApexParserTest
{
    [TestFixture]
    public class CSharpParserTests : TestFixtureBase
    {
        [Test]
        public void CSharpHelperParsesTheCSharpCodeAndReturnsTheSyntaxTree()
        {
            var unit = ApexSharpParser.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq.Think;
                using System.Text;
                using system.debug;

                namespace HelloWorld
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, World!"");
                        }
                    }
                }");

            Assert.NotNull(unit);

            var txt = ApexSharpParser.ToCSharp(unit);
            Assert.NotNull(txt);
        }

        [Test]
        public void SampleWalkerDisplaysTheSyntaxTreeStructure()
        {
            var unit = ApexSharpParser.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq.Think;
                using System.Text;
                using system.debug;

                namespace Demo
                {
                    struct Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, World!"");
                        }
                    }
                }");

            var walker = new SampleWalker();
            unit.Accept(walker);

            var tree = walker.ToString();
            Assert.False(string.IsNullOrWhiteSpace(tree));
        }

        [Test]
        public void CSharpHelperConvertsCSharpTextsToApex()
        {
            var csharp = "class Test1 { public Test1(int x) { } } class Test2 : Test1 { private int x = 10; }";
            var apexClasses = ApexSharpParser.ToApex(csharp);
            Assert.AreEqual(2, apexClasses.Length);

            CompareLineByLine(
                @"class Test1
                {
                    public Test1(Integer x)
                    {
                    }
                }", apexClasses[0]);

            CompareLineByLine(
                @"class Test2 extends Test1
                {
                    private Integer x = 10;
                }", apexClasses[1]);
        }

        [Test]
        public void CSharpHelperConvertsCSharpTextsToApexDictionary()
        {
            var csharp = @"
                class Test1 { public Test1(int x) { } }
                class Test2 : Test1 { private int x = 10; }
                enum SomeEnum { None, Unknown, Default }";

            var apexClasses = ApexSharpParser.ConvertToApex(csharp);
            Assert.AreEqual(3, apexClasses.Count);

            CompareLineByLine(
                @"class Test1
                {
                    public Test1(Integer x)
                    {
                    }
                }", apexClasses["Test1"]);

            CompareLineByLine(
                @"class Test2 extends Test1
                {
                    private Integer x = 10;
                }", apexClasses["Test2"]);

            CompareLineByLine(
                @"enum SomeEnum
                {
                    None,
                    Unknown,
                    Default
                }", apexClasses["SomeEnum"]);
        }
    }
}
