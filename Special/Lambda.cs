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
                 cadr   = cdr.getCar  (),   // list 1
                 cddr   = cdr.getCdr  (),
                 caddr  = cddr.getCar (),   // list 2
                 cdddr  = cddr.getCdr ();   // nil

            Console.Write("(");
            car.print(n, true);    // lambda
            Console.Write(" ");

            cadr.print(n++, false);   //    (= n 0)


            for (int i = 0; i < n; i++)
                Console.Write("    ");


            if (!caddr.isPair()){
                for (int i = 0; i < n; i++)
                    Console.Write("    ");
            }

            caddr.print(0, false);  //            1

            if (!caddr.isPair()){
                Console.WriteLine();
            }

            cdddr.print(n, true); //                )
            Console.WriteLine();

        }

    }
}
