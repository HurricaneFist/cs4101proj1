// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            // Indent

            for (int i = 0; i < n; i++) {
                Console.Write("    ");
            }
            
        }
    }
}
