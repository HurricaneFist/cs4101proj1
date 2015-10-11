/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree {

    public class Define : Special {

        private bool isFunc = false;

        public Define() { }

        public override void print(Node t, int n, bool p) {

            // Get the car, cdr, cadr, cddr, caddr, and cdddr

            Node car    = t.getCar();   // Define identifier

            Node cdr    = t.getCdr();
            Node cadr   = cdr.getCar(); // Function or variable to define

            Node cddr   = cdr.getCdr();
            Node caddr  = cddr.getCar();// Define function or variable as this

            Node cdddr  = cddr.getCdr();// Nil

            // Indent (if necessary)

            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            // Print "(define "

            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // If the cadr (first parameter) is a cons node, then define is a
            // function definition, but print it on the same line either way

            if (cadr.isPair()) {
                isFunc = true;
            }
            cadr.print(0, false);

            // If it's a function definition, indent and print the definition
            // Decrease the indentation again to preserve original whitespace
            if (isFunc) {
                n++;
                caddr.print(n, false);
                n--;
            }

            // Otherwise, don't indent and just print the second param
            // on the same line
            else {
                Console.Write(" ");
                caddr.setFormToRegular();
                caddr.print(0, false);
            }

            // Print the final right parenthesis and carriage return
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
