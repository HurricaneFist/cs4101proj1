/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Node -- Base class for parse tree node objects
using System;

namespace Tree {

    public class Node {

		// The argument of print(int) is the number of characters to indent.
        // Every subclass of Node must implement print(int).
        public virtual void print(int n) { }

        // The first argument of print(int, bool) is the number of characters
        // to indent.  It is interpreted the same as for print(int).
        // The second argument is only useful for lists (nodes of classes
        // Cons or Nil).  For all other subclasses of Node, the boolean
        // argument is ignored.  Therefore, print(n,p) defaults to print(n)
        // for all classes other than Cons and Nil.
        // For classes Cons and Nil, print(n,TRUE) means that the open
        // parenthesis was printed already by the caller.
        // Only classes Cons and Nil override print(int, bool).
        public virtual void print(int n, bool p) { print(n); }

        public virtual string getName() { return ""; }

        public virtual void setFormToQuote(bool b, int d) { }

        public virtual void setForm(Special p) { }

        public virtual void setFormToRegular() { }

        public virtual void setFormToRegular(bool b) {}

        // Helper functions that test the type of a node and that extract some
        // information.

        public virtual bool isBool()   { return false; }  // BoolLit
        public virtual bool isNumber() { return false; }  // IntLit
        public virtual bool isString() { return false; }  // StringLit
        public virtual bool isSymbol() { return false; }  // Ident
        public virtual bool isNil()    { return false; }  // Nil
        public virtual bool isPair()   { return false; }  // Cons

        public virtual Node getCar() {
            Console.Error.WriteLine("Cannot get car of a node other than Cons");
            return null;
        }
        public virtual Node getCdr() {
            Console.Error.WriteLine("Cannot get cdr of a node other than Cons");
            return null;
        }
        public virtual void setCar(Node a) {
            Console.Error.WriteLine("Cannot set car of a node other than Cons");
        }
        public virtual void setCdr(Node d) {
            Console.Error.WriteLine("Cannot set cdr of a node other than Cons");
        }

    }
}
