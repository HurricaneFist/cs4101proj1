// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree {

    public class Cond : Special {

        public Cond() { }

        public override void print(Node t, int n, bool p) {
            // Indent (if necessary)
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            t.setFormToRegular();

            // Get the car and cdr
            Node    car = t.getCar(),
                    cdr = t.getCdr();

            Console.Write("(");     // (
            car.print(n, true);     // Condition
            Console.WriteLine();    // Carriage return
            n++;                    // Increase indentation

            while (!cdr.isNil()) {
                // Indent the parameters of cond
                for (int i = 0; i < n; i++) {
                    Console.Write("    ");
                }
                cdr.getCar().print(0, false);
                Console.WriteLine();
                cdr = cdr.getCdr();
            }

            n--;                    // Decrease indentation

            // Print the final right parenthesis and
            // Indent (if necessary)
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }
            cdr.print(0, true);
            Console.WriteLine();
        }

    }
}
