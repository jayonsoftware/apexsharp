﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class WhileStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhileStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhileStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression, Statement);

        public ExpressionSyntax Expression { get; set; }

        public StatementSyntax Statement { get; set; }
    }
}
