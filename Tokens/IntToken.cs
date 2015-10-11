/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// IntToken -- Token object for representing integer constants.
using System;

namespace Tokens {

    public class IntToken : Token {

        private int intVal;

        public IntToken(int i) : base(TokenType.INT) { intVal = i; }

        public override void print() { Console.WriteLine(intVal); }

        public override int getIntVal() { return intVal; }

    }
}
