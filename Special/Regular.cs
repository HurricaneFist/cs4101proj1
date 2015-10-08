// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree {

    public class Regular : Special {

        public Regular() { }

        public override void print(Node t, int n, bool p) {
            indent(n);

            if (!p) {
                Console.Write("(");
            }

            Node car = t.getCar();
            if (car.isNil()) {
                car.print(n, false);
            }
            else {
                car.print(n, true)
            }

            Node cdr = t.getCdr();
            if (cdr.isPair()) {
                Console.Write(" ");
            }
            cdr.print(n, true);

        }
    }

}
