// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree {

    public class Set : Special {

        public Set() { }

        // !!!!
        public override void print(Node t, int n, bool p) {

            if (!p) {
                Console.Write("(");
            }

            // If this is set!, shouldn't there only be two arguments after the set token?
            // For example, (set! x (+ 2 3))
            // --> Set the identifier x to be (+ 2 3)

            Node car = t.getCar();
            if (car.isNull()) {
                car.print(n, false);
            }
            else {
                car.print(n, true);
            }

            Node cdr = t.getCdr();
            if (cdr.isPair()) {
                Console.Write(" ");
            }
            cdr.print(n, true);
        }

    }
}
