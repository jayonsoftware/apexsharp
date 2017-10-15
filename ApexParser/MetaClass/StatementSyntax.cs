﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class StatementSyntax : BaseSyntax
    {
        public StatementSyntax(string body = null)
        {
            Kind = SyntaxType.Statement;
            Body = body;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitStatement(this);

        public bool IsEmpty => string.IsNullOrWhiteSpace(Body);

        public string Body { get; set; }

        public StatementSyntax WithComments(IEnumerable<string> comments)
        {
            if (comments != null)
            {
                CodeComments.AddRange(comments);
            }

            return this;
        }
    }
}