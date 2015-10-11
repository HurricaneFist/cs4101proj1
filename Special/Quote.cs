/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree {

    public class Quote : Special {

        // Declare variables that help determine pretty printing rules

        private bool isRoot = false;    // whether or not this quote is the root
        private bool charQuote = true;  // if this is '() rather than (quote )
        private  int depth;             // the current depth

        // Define constructors that affect the private variables differently

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

            // Indent if necessary

            for (int i = 0; i < n; i++)
                Console.Write("    ");

            // Since this form can be created from either the quote character,
            // '\'', or the quote string, "quote" we need two mutually
            // exclusive cases to handle each

            if (charQuote) {
                // If what is trying to be printed is the root node,
                // set all subsequent Cons nodes' forms to Quote
                // print "'("
                // if the cdr is not nil, recur down the tree
                //else it is nil, so print the RPARN
                if (isRoot) {
                    t.setFormToQuote(charQuote, depth);
                    t.getCar().print(0, false);
                    Console.Write("(");
                    if (!t.getCdr().isNil()) {
                        t = t.getCdr();
                        t.print(0, true);
                    }
                    else {
                        t.getCdr().print(0, true);
                    }
                }

                // This is not the root, so do these set of instructions:
                // If the car is a Cons node, print a LPAREN because it is the
                // start of an inner list.
                // Print the car, which will implicitly recur on its subtree
                // Print single whitespaces when necessary and
                // print the cdr which also implicity recurs on its subtree
                else {
                    if (t.getCar().isPair()) {
                        Console.Write("(");
                    }
                    t.getCar().print(0, false);
                    if (!t.getCdr().isNil() && !isRoot) {
                        Console.Write(" ");
                    }
                    t.getCdr().print(0, true);
                }

                // Write a carriage return only if the cdr is Nil and the
                // Cons node depth is 0, which means it is on far right side of
                // the tree
                if (t.getCdr().isNil() && depth == 0) {
                    Console.WriteLine();
                }
            }

            // Quote strings are handled very similary with the exception that
            // the first and last parentheses are not printed and "quote" will
            // pretty print out to "\'"
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
