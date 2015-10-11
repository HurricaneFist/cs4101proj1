// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree {

    public class Cons : Node {

        private Node car;
        private Node cdr;
		private Special form;
		private int cn;

        public Cons(Node a, Node d, int i) {
			cn = i;
            car = a;
            cdr = d;
            parseList();
        }

        public override void setForm(Special newForm) {
            form = newForm;
            if (car.isPair()) {
                car.setForm(newForm);
            }
            if (cdr.isPair()) {
                cdr.setForm(newForm);
            }
        }

        public override void setFormToRegular() {
            form = new Regular(true, false);
            if (car.isPair()) {
                car.setFormToRegular();
            }
            if (cdr.isPair()) {
                cdr.setFormToRegular();
            }
        }

        public override void setFormToRegular(bool b) {
            form = new Regular(true, b);
            if (car.isPair()) {
                car.setFormToRegular();
            }
            if (cdr.isPair()) {
                cdr.setFormToRegular(b);
            }
        }

        public override void setFormToQuote(bool b, int d) {
            form = new Quote(b, d);
            if (car.isPair()) {
                car.setFormToQuote(b, d+1);
            }
            if (cdr.isPair()) {
                cdr.setFormToQuote(b, d);
            }
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
                //Console.Write(cn + " " + name);

                if (name == "\'") {
					form = new Quote (true, true);
                    Console.WriteLine("  charQuote");
				}
                else if (String.Compare(name, "quote", true) == 0) {
                    form = new Quote(false, true);
                    Console.WriteLine("  strQuote");
                }
                else if (String.Compare(name, "lambda", true) == 0) {
					form = new Lambda ();
                    Console.WriteLine("  lambda");
				}
				else if (String.Compare(name, "begin", true) == 0) {
					form = new Begin ();
                    //Console.WriteLine("  begin");
				}
				else if (String.Compare(name, "if", true) == 0) {
					form = new If ();
                    Console.WriteLine("  if");
				}
				else if (String.Compare(name, "let", true) == 0) {
					form = new Let ();
                    Console.WriteLine("  let");
				}
                else if (String.Compare(name, "cond", true) == 0) {
                    form = new Cond ();
                    Console.WriteLine("  cond");
                }
				else if (String.Compare(name, "define", true) == 0) {
                    form = new Define();
                    Console.WriteLine("  define");
                }
				else if (String.Compare(name, "set!", true) == 0) {
					form = new Set ();
                    //Console.WriteLine("  set!");
				}
                else {
                    form = new Regular();
                    //Console.WriteLine("  reg");
                }
			}
            else {
                form = new Regular();
                //Console.WriteLine("  reg");
            }
        }

        public override void print(int n) {
            //Console.WriteLine("consprint1");
            form.print(this, n, false);
            /*
			Console.WriteLine("cons" + cn);
			Console.WriteLine("cons" + cn + "car");
			car.print(n);
            Console.WriteLine();
			Console.WriteLine("cons" + cn + "cdr");
			cdr.print(n);
            */
        }

        public override void print(int n, bool p) {
            //Console.WriteLine("consprint2");
            form.print(this, n, p);
        }
    }
}
