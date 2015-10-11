/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree {

    public class If : Special {

	public If() { }

        public override void print(Node t, int n, bool p) {

            // Indent (if necessary)
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Get the car, cdr, cadr, cddr, cdddr, cddddr, caddr, and cadddr

            Node car    = t.getCar    (),   // if

                 cdr    = t.getCdr    (),
                 cadr   = cdr.getCar  (),   // condition

                 cddr   = cdr.getCdr  (),
                 caddr  = cddr.getCar (),   // case if (condition)

                 cdddr  = cddr.getCdr (),
                 cadddr = cdddr.getCar(),   // case if (!condition)

                 cddddr = cdddr.getCdr();   // final right parenthesis

            // Print "(if "
            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // Print the condition
            cadr.print(0, false);

            // Increase indentation count and indent
            n++;
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Print the if (#t) expression. Indentation has already been
            // applied via n if it's a list or via previous if block
            caddr.print(0, false);

            // Print carriage return if first case is a not a list because
            // non-lists do not print white space
            if (!caddr.isPair()){
                Console.WriteLine();
            }

            // Set cadddr form to regular in order to print it on a single line
            if (cadddr.isPair()){
                cadddr.setForm(new Regular(false));
            }

            // Print indentation and the if (#f) expression.
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            cadddr.print(0, false);

            // Decrement indent count and carriage return
            n--;
            Console.WriteLine();

            // Print indentations if necessary
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Print the final right parenthesis
            cddddr.print(n, true);

            // And end with another carriage return
            Console.WriteLine();
        }

    }
}
