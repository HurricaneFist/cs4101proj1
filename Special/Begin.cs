// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree {

    public class Begin : Special {

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
            car.print(n, true);     // Begin
            Console.WriteLine();    // Carriage return
            n++;                    // Increase indentation

            t.setForm(new Regular(false));

            while (!cdr.isNil()) {
                // Indent the parameters of begin if necessary
                for (int i = 0; i < n; i++) {
                    Console.Write("    ");
                }
                // Print the cadr
                // (This implicitly recurs on the left of the tree)
                cdr.getCar().print(0, false);
                Console.WriteLine();
                // Recur on the right of the tree.
                cdr = cdr.getCdr();
            }

            // Print the final right parenthesis with no indentation
            // and carriage return
            cdr.print(0, true);
            Console.WriteLine();
        }

    }
}
