// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree {

    public class StringLit : Node {

        private string stringVal;

        public StringLit(string s) {
            stringVal = s;
            //Console.WriteLine("stringLitNode " + s);
        }

		public override bool isString() { return true; }

        public override void print(int n) {
            //indent(n);
            Console.Write("\"" + stringVal + "\"");
        }

    }
}
