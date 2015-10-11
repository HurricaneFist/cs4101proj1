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
                 cdddr  = cddr.getCdr (),   // nil
                 caddr  = cddr.getCar ();   // list2

            // Print "(lambda "
            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // Print list1
            cadr.print(0, false);

            n++;

            // Print list2 and indent for it
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            caddr.print(n, false);

            n--;

            // Indent (if necessary) and print the final right parenthesis
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
