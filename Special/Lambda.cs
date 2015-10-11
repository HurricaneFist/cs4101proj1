/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree {

    public class Lambda : Special {

        public Lambda() { }

        public override void print(Node t, int n, bool p) {

            // Indent (if necessary)

            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Get the car, cdr, cadr, cddr, cdddr, caddr

            Node car    = t.getCar    (),   // lambda

                 cdr    = t.getCdr    (),
                 cadr   = cdr.getCar  (),   // list1

                 cddr   = cdr.getCdr  (),
                 caddr  = cddr.getCar (),   // list2

                 cdddr  = cddr.getCdr ();   // nil


            // Print "(lambda "
            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // Print list1
            cadr.print(0, false);

            // Increase indentation count
            n++;

            // Print list2 with indents
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            caddr.print(n, false);

            // Decrease indentation count
            n--;

            // Indent (if necessary) and print the final right parenthesis
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Print final right parenthesis and carriage return
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
