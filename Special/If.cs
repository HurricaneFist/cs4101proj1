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
                 cdddr  = cddr.getCdr (),
                 cddddr = cdddr.getCdr(),   // final right parenthesis
                 caddr  = cddr.getCar (),   // case if (condition)
                 cadddr = cdddr.getCar();   // case if (!condition)

            // Print "(if "
            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            // Print the condition
            cadr.print(0, false);

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
            }

            if (!caddr.isPair()){
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
                n--;
            }

            cadddr.print(0, false);

            Console.WriteLine();

            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Print the final right parenthesis
            cddddr.print(n, true);
            Console.WriteLine();
        }

    }
}
