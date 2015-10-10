// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {

	public If() { }

        public override void print(Node t, int n, bool p)
        {
            
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            
            Node car    = t.getCar    (), 
                 cdr    = t.getCdr    (), 
                 cadr   = cdr.getCar  (),                  
                 cddr   = cdr.getCdr  (),
                 cdddr  = cddr.getCdr (),
                 cddddr = cdddr.getCdr(), // nil
                 caddr  = cddr.getCar (), 
                 cadddr = cdddr.getCar();
                 
            car.print(n, true);    // (if
            Console.Write(" ");
            
            cadr.print(n, true);   //    (= n 0)
            Console.WriteLine();
            
            caddr.print(++n, true);  //            1
            Console.WriteLine();
            
            cadddr.print(n, true); //              2
            Console.WriteLine();
            
            cddddr.print(--n, true); //                )
            
            /*
            Node car  = t.getCar();
            Node cdr  = t.getCdr();
            Node cadr = cdr.getCar();
            Node cddr = cdr.getCdr();
            Node caddr = cddr.getCar();
            Node cdddr = cddr.getCdr(); // Must be Nil
            bool isFunc = false;

            if (cadr.isPair()){
                isFunc = true;
            }

            if (!p) {
                Console.Write("(");
            }

            car.print(n, true);
            cadr.print(n, true);
            Console.WriteLine();

            // Indent stuff
            if (isFunc) {
                n++;
            }

            caddr.print(n, true);

            if (isFunc) {
                n--;
            }

            cdddr.print(n, true);
            */
        }
    }
}
