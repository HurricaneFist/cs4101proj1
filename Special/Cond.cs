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

            // Get the car and cdr
            Node    car = t.getCar(),
                    cdr = t.getCdr();

            Console.Write("(");     // (
            car.print(n, true);     // Condition
            Console.WriteLine();    // Carriage return
            n++;                    // Increase indentation for the
                                    // subsequent arguments

            t.setForm(new Regular(false));

            while (!cdr.isNil()) {  // Print cond's conditions and expressions
                for (int i = 0; i < n; i++) {   // Indent the parameters of cond
                    Console.Write("    ");
                }
                cdr.getCar().print(0, false);   // Implicitly recur on cars
                Console.WriteLine();            // Carriage return
                cdr = cdr.getCdr();     // Recur down the right side of the tree
            }

            // Decrease indentation because only the arguments needed to
            // be indented
            n--;

            // Indent (if necessary), print the final right parenthesis and the
            // final carriage return
            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }
            cdr.print(0, true);
            Console.WriteLine();
        }

    }
}
