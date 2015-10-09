// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree {

    public class Quote : Special {

	    public Quote() { }

        public override void print(Node t, int n, bool p) {
            
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            
            Node car  = t.getCar  (),
                 cdr  = t.getCdr  (),
                 cddr = cdr.getCdr(), // should always be nil
                 cadr = cdr.getCar(); // the X in (quote X)
                 
            if (!p)
                Console.Write("(");
            
            car.print(n, true);  // (quote 
            cadr.print(n, true); //       (set! x 4)
            cddr.print(n, true); //                 )   
         
            
        }

    }
}
