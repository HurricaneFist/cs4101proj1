// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree {

    public class Regular : Special {
        private bool regulate = false, hasMessage = false;
        public int depth = 0;

        public Regular() { }

        public Regular(bool r, bool quoteString) {
            regulate = r;
            hasMessage = quoteString;
        }

        // Boolean p - true if left parenthesis HAS BEEN PRINTED
        //             false if '(' has not been printed

        public override void print(Node t, int n, bool p) {

            // Print indentations

            for (int i = 0; i < n; i++) {
                Console.Write("    ");  // Four spaces
            }

            if (!p) {                   // If '(' has not been printed
                Console.Write("(");     // Print one
            }

            // Regulate the entire subtree
            if (regulate) {
                t.getCdr().setFormToRegular();
            }
            //Console.WriteLine("$$$");
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
            //Console.WriteLine("@@@");


            // Print a space if we have not reached the end of the list
            // Then print the cdr
            if (!t.getCdr().isNil()) {
                Console.Write(" ");
            }
            //Console.WriteLine("###");
            //Console.WriteLine(t.getCdr().isPair());
            Node cdr = t.getCdr();
            if (cdr.isNil()) {
                if (!hasMessage) {
                    cdr.print(n, true);
                    if (regulate) {
                        depth--;
                    }
                    if (depth == 0) {
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
