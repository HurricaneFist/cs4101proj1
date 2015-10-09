// BoolLit -- Parse tree node class for representing boolean literals

using System;

namespace Tree {

    public class BoolLit : Node {

        private bool boolVal;

        public BoolLit(bool b) {
            boolVal = b;
        }

		public override bool isBool() {
            return true;
        }

        public override void print(int n) {
            /*
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }
            */
            if (boolVal)
                Console.Write("#t");
            else
                Console.Write("#f");
        }

    }
}
