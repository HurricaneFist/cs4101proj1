// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree {

    public class Define : Special {
        private bool isFunc = false;

        public Define() { }

        public override void print(Node t, int n, bool p) {

            // Get the car, cdr, cadr, cddr, caddr, and cdddr

            Node car    = t.getCar();
            Node cdr    = t.getCdr();

            Node cadr   = cdr.getCar();
            Node cddr   = cdr.getCdr();

            Node caddr  = cddr.getCar();
            Node cdddr  = cddr.getCdr();

            // Indent (if necessary)

            for (int i = 0; i < n; i++){
                Console.Write("    ");
            }

            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // If the cadr (first parameter) is a cons node, then define is a
            // function definition

            if (cadr.isPair()) {
                isFunc = true;
            }
            cadr.print(0, false);

            if (isFunc) {
                // If it's a function definition, indent and print the
                // definition
                n++;
                caddr.print(n, false);
                n--;
            }
            else { // Otherwise, don't indent and just print the second param
                caddr.print(0, false);
            }

            // Print the final right parenthesis
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
