// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {

	public If() { }

        public override void print(Node t, int n, bool p) {
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            Node car    = t.getCar    (),   // if
                 cdr    = t.getCdr    (),
                 cadr   = cdr.getCar  (),   // condition
                 cddr   = cdr.getCdr  (),
                 cdddr  = cddr.getCdr (),
                 cddddr = cdddr.getCdr(), // nil
                 caddr  = cddr.getCar (),   // true
                 cadddr = cdddr.getCar();   // false

            car.print(n, true);    // (if
            Console.Write(" ");

            cadr.print(n, false);   //    (= n 0)

            caddr.print(++n, false);  //            1
            Console.WriteLine();

            cadddr.print(n, false); //              2
            Console.WriteLine();

            cddddr.print(--n, true); //                )
            Console.WriteLine();
        }
    }
}
