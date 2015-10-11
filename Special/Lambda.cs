// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree {

    public class Lambda : Special {

        public Lambda() { }

        public override void print(Node t, int n, bool p) {
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            Node car    = t.getCar    (),   // lambda
                 cdr    = t.getCdr    (),
                 cadr   = cdr.getCar  (),   // list1
                 cddr   = cdr.getCdr  (),
                 cdddr  = cddr.getCdr (), // nil
                 caddr  = cddr.getCar ();   // list2

            Console.Write("(");
            car.print(0, true);    // (lambda
            Console.Write(" ");

            cadr.print(0, false);   //    list1
/*
            if (!caddr.isPair()){
                n++;
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
            }*/

            n++;

            for (int i = 0; i < n; i++)
                Console.Write("    ");
            caddr.print(n, false); //list2

            n--;

            for (int i = 0; i < n; i++)
                Console.Write("    ");
                
            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
