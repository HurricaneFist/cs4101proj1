// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        public int numIndents = 0;

		public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            // Indent

            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            Console.Write("(begin\n");
            n++;

            // Indent again

            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }

            t.getCdr().print(n, true);
        }
    }
}
