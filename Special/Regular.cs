// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree {

    public class Regular : Special {
        private bool regulate = false, hasMessage = false, CR = true;
        public int depth = 0;

        public Regular() { }

        public Regular(bool cr) {
            CR = cr;
        }

        public Regular(bool r, bool quoteString) {
            regulate = r;
            hasMessage = quoteString;
        }

        // Boolean p - true if left parenthesis HAS BEEN PRINTED
        //             false if '(' has not been printed

        public override void print(Node t, int n, bool p) {

            if (!p) {                   // If '(' has not been printed
                Console.Write("(");     // Print one
            }

            // Regulate the entire subtree
            if (regulate) {
                t.getCdr().setFormToRegular();
            }

            // Print car while handling empty lists inside of lists
            if (t.getCar().isNil()) {
                Console.Write("()");
            }
            else {
                Node car = t.getCar();
                if (regulate && car.isPair()){
                    depth++;
                }
                car.print(n, false);
            }


            // Print a space if we have not reached the end of the list
            // Then print the cdr
            if (!t.getCdr().isNil()) {
                Console.Write(" ");
            }
            Node cdr = t.getCdr();
            if (cdr.isNil()) {
                if (!hasMessage) {
                    cdr.print(n, true);
                    if (regulate) {
                        depth--;
                    }
                    //Console.WriteLine("CR = " + CR);
                    if (depth == 0 && CR) {
                        Console.WriteLine();
                    }
                }

            }
            else {
                cdr.print(n, true);
            }

        }
    }
}
