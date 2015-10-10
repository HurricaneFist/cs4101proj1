// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree {

    public class Set : Special {

        public Set() { }

        public override void print(Node t, int n, bool p) {

            for (int i = 0; i < n; i++)
                Console.Write("    ");

			Node car   = t.getCar   (),  // set!
                 cdr   = t.getCdr   (),
                 cadr  = cdr.getCar (),  // ident
                 cddr  = cdr.getCdr (),
                 caddr = cddr.getCar(),  // ident, string, int, or list
                 cdddr = cddr.getCdr();  // nil

            if (!p)
                Console.Write("(");
            t.setFormToRegular();
            car.print(n, true);
            Console.Write(" ");
            cadr.print(n, true);
            Console.Write(" ");

            caddr.print(n, false);
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
