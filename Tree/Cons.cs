// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
    public class Cons : Node
	{
        private Node car;
        private Node cdr;
		private Special form;
		private int cn;
    
        public Cons(Node a, Node d, int i)
        {
			cn = i;
            car = a;
            cdr = d;
			parseList();
        }
   		
		public override bool isPair() { return true; }

		public override Node getCar() { return car; }

		public override Node getCdr() { return cdr; }

        // parseList() `parses' special forms, constructs an appropriate
        // object of a subclass of Special, and stores a pointer to that
        // object in variable form.  It would be possible to fully parse
        // special forms at this point.  Since this causes complications
        // when using (incorrect) programs as data, it is easiest to let
        // parseList only look at the car for selecting the appropriate
        // object from the Special hierarchy and to leave the rest of
        // parsing up to the interpreter.
        void parseList()
        {
            // TODO: implement this function and any helper functions
            // you might need.
        }
 
        public override void print(int n)
        {
            //form.print(this, n, false);
			Console.WriteLine("cons" + cn);
			Console.WriteLine("cons" + cn + "car");
			getCar().print(n);
			Console.WriteLine("cons" + cn + "cdr");
			getCdr ().print (n);
        }

        public override void print(int n, bool p)
        {
            form.print(this, n, p);
        }
    }
}

