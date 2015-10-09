// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree {

    public class Regular : Special {

        public Regular() { }

        // Boolean p - true if left parenthesis HAS BEEN PRINTED
        //             false if '(' has not been printed

        public override void print(Node t, int n, bool p) {

            // Print indentations

            for (int i = 0; i < n; i++) {
                Console.Write("    ");  // Four spaces
            }

            if (!p) {                   // If '(' has not been printed
                Console.Write("(");     // Print one
            }

            // Print car

            Node car = t.getCar();
            if (car.isNil()) {     // If car isNull, it is an empty list
                //car.print(n, false);
                Console.Write("()");// So print the empty list
            }
            else {
                car.print(n, true); // Else print the node such that the
                                    // left parenthesis has already been printed
            }

            // Print cdr

            Node cdr = t.getCdr();
            if (cdr.isPair()) {
                Console.Write(" ");
            }
            cdr.print(n, true);
            if (cdr.isNil()) {
                Console.WriteLine();
            }

            // End with a carriage return

        }
    }
}
