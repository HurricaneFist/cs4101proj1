// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree {

    public class Let : Special {

        public Let() { }

        public override void print(Node t, int n, bool p) {

            // Indent (if necessary)

            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Get the car and cdr

            Node car = t.getCar(),
                 cdr = t.getCdr();

            // Print "(begin "
            Console.Write("(");
            car.print(n, true);
            Console.WriteLine();
            n++;

            while (!cdr.isNil()) {
                // Indent and print all parameters (cadrs) recursively
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
                cdr.getCar().print(0, false);
                cdr = cdr.getCdr();
            }

            // Print the final right parenthesis

            cdr.print(0, true);
            Console.WriteLine();
        }
    }
}
