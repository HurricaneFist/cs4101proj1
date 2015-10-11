/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree {

    public class Cons : Node {

        private Node car;
        private Node cdr;
		private Special form;

        public Cons(Node a, Node d) {
            car = a;
            cdr = d;
            parseList();
        }

        public override void setForm(Special newForm) {
            form = newForm;
            if (car.isPair()) { car.setForm(newForm); }
            if (cdr.isPair()) { cdr.setForm(newForm); }
        }

        public override void setFormToRegular() {
            form = new Regular(true, false);
            if (car.isPair()) { car.setFormToRegular(); }
            if (cdr.isPair()) { cdr.setFormToRegular(); }
        }

        public override void setFormToRegular(bool b) {
            form = new Regular(true, b);
            if (car.isPair()) { car.setFormToRegular();  }
            if (cdr.isPair()) { cdr.setFormToRegular(b); }
        }

        public override void setFormToQuote(bool b, int d) {
            form = new Quote(b, d);
            if (car.isPair()) { car.setFormToQuote(b, d+1); }
            if (cdr.isPair()) { cdr.setFormToQuote(b, d);   }
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
        public void parseList() {
            if (car.isSymbol()) {
                string name = car.getName();
                if (name == "\'") { form = new Quote (true, true); }
                else if (String.Compare(name,  "quote", true) == 0) { form = new Quote(false, true); }
                else if (String.Compare(name, "lambda", true) == 0) { form = new Lambda (); }
				else if (String.Compare(name,  "begin", true) == 0) { form = new Begin (); }
				else if (String.Compare(name,     "if", true) == 0) { form = new If (); }
				else if (String.Compare(name,    "let", true) == 0) { form = new Let (); }
                else if (String.Compare(name,   "cond", true) == 0) { form = new Cond (); }
				else if (String.Compare(name, "define", true) == 0) { form = new Define(); }
				else if (String.Compare(name,   "set!", true) == 0) { form = new Set (); }
                else { form = new Regular(); }
			}
            else { form = new Regular(); }

        }

        public override void print(int n)         { form.print(this, n, false);}

        public override void print(int n, bool p) { form.print(this, n, p);    }

    }
}
