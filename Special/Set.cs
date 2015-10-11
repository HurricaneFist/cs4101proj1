/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree {

    public class Set : Special {

        public Set() { }

        public override void print(Node t, int n, bool p) {

            // Indent (if necessary)

            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Get the car, cdr, cadr, cddr, caddr, cdddr

		    Node car   = t.getCar   (), // set!

                 cdr   = t.getCdr   (),
                 cadr  = cdr.getCar (), // ident

                 cddr  = cdr.getCdr (),
                 caddr = cddr.getCar(), // ident, string, int, or list

                 cdddr = cddr.getCdr(); // nil

            // If the left parenthesis has not yet been printed, print it

            if (!p)
                Console.Write("(");


            t.setFormToRegular();
            car.print(n, true);     // set!
            Console.Write(" ");
            cadr.print(n, true);    // param1
            Console.Write(" ");

            caddr.print(n, false);  // param2

            // Print the final right parenthesis and carriage return
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
