// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree {

    public class Define : Special {

        public Define() { }

        // !!!!
        public override void print(Node t, int n, bool p) {
            indent(n);

            if (!p) {
                Console.Write("(");
            }

            // If this is define, shouldn't there only be two arguments after the define token?
            // For example, (define x 0)
            // --> Define the identifier x to be 0.

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
