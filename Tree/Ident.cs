/*
Project Members:
    Bobby Kong,
    Ian Lee
Dr. Gerald Baumgartner
CSC 4101, Section 1
October 11, 2015
*/

// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree {

    public class Ident : Node {

        private string name;

        public Ident(string n) { name = n; }

        public override string getName() { return name; }

		public override bool isSymbol() { return true; }

        public override void print(int n) { Console.Write(name); }

    }
}
