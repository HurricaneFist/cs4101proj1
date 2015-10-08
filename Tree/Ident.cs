// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree {

    public class Ident : Node {

        private string name;

        public Ident(string n) {
            name = n;
        }

		public override bool isSymbol() {
            return true;
        }

        public override void print(int n) {
            Special.indent(n)
            //for (int i = 0; i < n; i++)
            //    Console.Write(" ");

            Console.WriteLine(name);
        }

    }
}
