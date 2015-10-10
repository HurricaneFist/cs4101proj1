// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree {

    public class Quote : Special {
        private bool quoteString = false;

	    public Quote() { }

        public Quote(bool b) {
            quoteString = b;
        }

        public override void print(Node t, int n, bool p) {
            for (int i = 0; i < n; i++)
                Console.Write("    ");

            if (!p)
                Console.Write("'(");

            t = t.getCdr();
            t.setFormToRegular(quoteString);

            t.print(n, true);
        }

    }
}
