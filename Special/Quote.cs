// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree {

    public class Quote : Special {
        
        private bool isRoot = false;
        private bool charQuote = true; // strQuote = !charQuote
        private  int depth;

        public Quote (bool b, int d) {
            charQuote = b;
            depth = d;
        }

        public Quote (bool b, bool root) {
            charQuote = b;
            isRoot = root;
            depth = 0;
        }

        public override void print(Node t, int n, bool p) {
            for (int i = 0; i < n; i++)
                Console.Write("    ");
            if (charQuote) {
                if (isRoot) {
                    t.setFormToQuote(charQuote, depth);
                    t.getCar().print(0, false);
                    Console.Write("(");
                    if (!t.getCdr().isNil()) {
                        t = t.getCdr();
                    }
                    t.print(0, true);
                }
                else {
                    if (t.getCar().isPair()) {
                        //depth++;
                        Console.Write("(");
                    }
                    t.getCar().print(0, false);
                    if (!t.getCdr().isNil() && !isRoot) {
                        Console.Write(" ");
                    }
                    else {
                        //depth--;
                    }
                    t.getCdr().print(0, true);
                }
                if (t.getCdr().isNil() && depth == 0) {
                    Console.WriteLine();
                }
            }
            else {
                if (isRoot) {
                    t.setFormToQuote(charQuote, depth);
                    Console.Write("\'");
                    t = t.getCdr();
                    t.print(0, true);
                }
                else {
                    if (t.getCar().isPair()) {
                        Console.Write("(");
                    }
                    t.getCar().print(0, false);
                    if (!t.getCdr().isNil()) {
                        Console.Write(" ");
                        t.getCdr().print(0, true);
                    }
                    else {
                        if (depth != 0) {
                            t.getCdr().print(0, true);
                        }
                        else {
                            Console.WriteLine();
                        }
                    }
                }
            }

        }

    }
}
