// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Lambda() { }

        public override void print(Node t, int n, bool p)
        {
            Node car  = t.getCar();
            Node cdr  = t.getCdr();
            Node cadr = cdr.getCar();
            Node cddr = cdr.getCdr();
            Node caddr = cddr.getCar();
            Node cdddr = cddr.getCdr(); // Must be Nil
            bool isFunc = false;

            if (cadr.isPair()){
                isFunc = true;
            }

            if (!p) {
                Console.Write("(");
            }

            car.print(n, true);
            cadr.print(n, true);
            Console.WriteLine();

            // Indent stuff
            if (isFunc) {
                n++;
            }

            caddr.print(n, true);

            if (isFunc) {
                n--;
            }

            cdddr.print(n, true);
  	    }
    }
}
