/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree {

    public class StringLit : Node {

        private string stringVal;

        public StringLit(string s) { stringVal = s; }

		public override bool isString() { return true; }

        public override void print(int n) { Console.Write("\"" + stringVal + "\""); }

    }
}
