// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree {

    public class Begin : Special {

        public int numIndents = 0; // ***NOTE: this variable seems unused

		    public Begin() { }

        public override void print(Node t, int n, bool p) {
            // Indent (if necessary)
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            // Get the car and cdr
            Node    car = t.getCar(),
                    cdr = t.getCdr();

            Console.Write("(");     // (
            car.print(n, true);     // begin
            Console.WriteLine();
            n++;

            while (!cdr.isNil()) {
                // Indent the parameters of begin
                for (int i = 0; i < n; i++) {
                    Console.Write("    ");
                }
                cdr.getCar().print(0, false);
                cdr = cdr.getCdr();
            }

            // Print the final right parenthesis
            cdr.print(0, true);
            Console.WriteLine();
        }

    }
}
