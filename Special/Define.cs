// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree {

    public class Define : Special {

        public Define() { }

        public override void print(Node t, int n, bool p) {

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
            Console.Write(" ");
            cadr.print(n, true);


            // Indent stuff
            if (isFunc) {
                Console.WriteLine();
                n++;
            }
            else {
                Console.Write(" ");
            }

            caddr.print(n, true);

            if (isFunc) {
                n--;
            }

            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
