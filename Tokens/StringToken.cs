/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// StringToken -- Token object for representing string constants.
using System;

namespace Tokens {

    public class StringToken : Token {

        private string stringVal;

        public StringToken(string s) : base(TokenType.STRING) { stringVal = s; }

        public override void print() { Console.WriteLine(stringVal); }

        public override string getStringVal() { return stringVal; }

    }
}
