// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree {

    public class Begin : Special {

        public int numIndents = 0;

		public Begin() { }

        public override void print(Node t, int n, bool p) {
            // Indent
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            Node    car = t.getCar(),
                    cdr = t.getCdr();

            Console.Write("(");     // (
            car.print(n, true);     // begin
            Console.WriteLine();
            n++;
            while (!cdr.isNil()) {
                // Indent
                for (int i = 0; i < n; i++) {
                    Console.Write("    ");
                }
                cdr.getCar().print(0, false);
                cdr = cdr.getCdr();
            }

            cdr.print(0, true);
            Console.WriteLine();

        }
    }
}
