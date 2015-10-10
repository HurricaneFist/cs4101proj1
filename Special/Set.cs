// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree {

    public class Set : Special {

        public Set() { }

        public override void print(Node t, int n, bool p) {

            for (int i = 0; i < n; i++)
                Console.Write("    ");

			Node car   = t.getCar   (),
                 cdr   = t.getCdr   (),
                 cadr  = cdr.getCar (),
                 cddr  = cdr.getCdr (),
                 caddr = cddr.getCar(),
                 cdddr = cddr.getCdr(); // Which should be Nil

            if (!p)
                Console.Write("(");

            car.print(n, true);
            Console.Write(" ");
            cadr.print(n, true);
            Console.Write(" ");
            caddr.print(n, true);
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
