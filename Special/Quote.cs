// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree {

    public class Quote : Special {

	    public Quote() { }

        public override void print(Node t, int n, bool p) {
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            if (!p) {
                Console.Write("\'(");
            }

            // Print Car

            Node car = t.getCar();
            if (car.isNull()) {         // If car isNull, it is an empty list
                car.print(n, false);
            }
            else {
                car.print(n, true);     // Else print the node such that the left parenthesis has already been printed
            }

            // Print cdr

            Node cdr = t.getCdr();
            if (cdr.isPair()) {
                Console.Write(" ");
            }
            cdr.print(n, true);
        }

    }
}
