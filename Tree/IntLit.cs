// IntLit -- Parse tree node class for representing integer literals

using System;

namespace Tree {

    public class IntLit : Node {

        private int intVal;

        public IntLit(int i) {
            intVal = i;
            //Console.WriteLine("intLitNode " + i);
        }

		public override bool isNumber() { return true; }

        public override void print(int n) {
            //indent(n);
            Console.Write(intVal);
        }

    }
}
