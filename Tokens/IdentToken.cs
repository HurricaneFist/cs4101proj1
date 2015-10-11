/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// IdentToken -- Token object for representing identifiers.
using System;

namespace Tokens {

    public class IdentToken : Token {

        private string name;

        public IdentToken(string s) : base(TokenType.IDENT) { name = s; }

        public override void print() { Console.WriteLine(name); }

        public override string getName() { return name; }

    }
}
