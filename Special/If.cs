// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree {

    public class If : Special {

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

            Console.Write("(");
            car.print(0, true);    // (if
            Console.Write(" ");

            cadr.print(0, false);   //    (= n 0)

            if (!caddr.isPair()){
                n++;
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
            }

            caddr.print(n, false);
            if (!caddr.isPair()){
                Console.WriteLine();
            }
            if (cadddr.isPair()){
                cadddr.setForm(new Regular(false));
                //for (int i = 0; i < n; i++)
                //    Console.Write("    ");
            }
            if (!caddr.isPair()){
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
                n--;
            }
            cadddr.print(0, false);

            if (!cadddr.isPair()){
            //    Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            cddddr.print(n, true);
            Console.WriteLine();
        }

    }
}
