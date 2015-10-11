// IdentToken -- Token object for representing identifiers.
using System;

namespace Tokens {

    public class IdentToken : Token {

        private string name;

        public IdentToken(string s) : base(TokenType.IDENT) { name = s; }

        public override void print() { Console.WriteLine(name); }

        public override string getName() { return name; }

    }
}
