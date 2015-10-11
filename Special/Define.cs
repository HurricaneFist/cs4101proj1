// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree {

    public class Define : Special {
        private bool isFunc = false;

        public Define() { }

        public override void print(Node t, int n, bool p) {
            Node car    = t.getCar();
            Node cdr    = t.getCdr();

            Node cadr   = cdr.getCar();
            Node cddr   = cdr.getCdr();

            Node caddr  = cddr.getCar();
            Node cdddr  = cddr.getCdr();

            for (int i = 0; i < n; i++){
                Console.Write("    ");
            }

            Console.Write("(");
            car.print(0, true);
            Console.Write(" ");

            if (cadr.isPair()) {
                isFunc = true;
            }
            cadr.print(0, false);
            if (isFunc){
                n++;
                caddr.print(n, false);
                n--;
            }
            else{
                Console.Write(" ");
                caddr.setFormToRegular();
                caddr.print(0, false);
            }

            cdddr.print(n, true);
            Console.WriteLine();
        }

    }
}
