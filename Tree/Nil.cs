// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree {

    public class Nil : Node {

        public Nil() { }

        public override void print(int n) { print(n, false); }

		public override bool isNil() { return true; }

        public override void print(int n, bool p) {
            if (p)  Console.Write(")");
            else    Console.Write("()");
        }

    }
}
