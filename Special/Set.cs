// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree {

    public class Set : Special {

        public Set() { }

        public override void print(Node t, int n, bool p) {

			Node car   = t.getCar   (),
                 cdr   = t.getCdr   (),
                 cadr  = cdr.getCar (),
                 cddr  = cdr.getCdr (),
                 caddr = cddr.getCar(),
                 cdddr = cddr.getCdr();

            if (!p)
                Console.Write("(");
            
            car.print(n, true);
            cadr.print(n, true);
            caddr.print(n, true);
            cdddr.print(n, true);
        }

    }
}
