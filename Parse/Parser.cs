// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL token),
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse {

	public class Parser {

		// Take a Scanner object as input
        private Scanner scanner;

		// Tool used to keep track of cons nodes for debugging
		public int cn = 0;

		// Initialize nodes that only ever need to be created once
		// All false, true, and nil nodes will simply be pointers to these nodes

		public Node nodeFalse = new BoolLit(false);
		public Node nodeTrue  = new BoolLit(true);
		public Node nodeNil   = new Nil();

		// Implement constructor

        public Parser(Scanner s) { scanner = s; }

		// Takes a token as input and makes a node out of it
		public Node makeExp (Token t) {
			if (t == null) return nodeNil;

			TokenType tt = t.getType();

			// If this token is a LPAREN, parse the rest of the list
			if (tt == TokenType.LPAREN)     { Console.WriteLine(); return parseRest(); }

			// If token is TRUE or FALSE,
			// return pointers to the nodes we initialized at the beginning
			else if (tt == TokenType.FALSE) { Console.WriteLine(); return nodeFalse; }
			else if (tt == TokenType.TRUE)  { Console.WriteLine(); return nodeTrue; }

			// If this token in a QUOTE, make a Cons node
			// with ' as its car
			// and the parsing of a node (or node tree) as its cdr
			// because what follows a QUOTE token should be treated
			// as a regular list to parse.
			else if (tt == TokenType.QUOTE)	{
				Console.WriteLine();
				return new Cons (
					new Ident ("\'"),
					parseExp (),
					cn++
				);
			}

			// For INT, STRING, and IDENT tokens,
			// just return their respective nodes while keeping node data
			// consistent with the original token values
			else if (tt == TokenType.INT)    return new IntLit(t.getIntVal());
			else if (tt == TokenType.STRING) return new StringLit(t.getStringVal());
			else /*(tt == TokenType.IDENT)*/ return new Ident(t.getName());

		}

		// Get the next token, make a node out of it, and return that node
        public Node parseExp() {
			Token t = scanner.getNextToken();

			Console.Write(t.getType());
			TokenType tt = t.getType();
			Console.Write(" ");
			t.print();

			return makeExp (t);
        }

		// Take a token as input, make a node out of it, and return that node
		public Node parseExp(Token t) {
			return makeExp (t);
		}

		public Node makeRest(Token t1) {
			// We need the type of this token so we can find out
			// what kind of nodes we need to make and where to put them
			TokenType tt1 = t1.getType();

			Console.Write(tt1 + " ");
			if (tt1 == TokenType.LPAREN) {
				Console.WriteLine();
			}
			t1.print();

			// If this token is a RPAREN, just return Nil
			if (tt1 == TokenType.RPAREN) {
				Console.WriteLine();
				return nodeNil;
			}

			// Otherwise, find out what the next token and its type is
			Token t2 = scanner.getNextToken();
			TokenType tt2 = t2.getType();

			Console.Write(tt2 + " ");
			t2.print();

			// If this token is an exp and the next token is a DOT,
			// finish parsing this exp in the car of a new Cons node,
			// and parse the next-next token as an exp
			// because we know it will be the last exp since it follows a DOT
			if (tt2 == TokenType.DOT) {
				Console.WriteLine();
				return new Cons (parseExp(t1), parseExp(), cn++);
			}

			// If the next token is a RPAREN,
			// we can infer that it will be the end of the list, and we can
			// return a Cons node that parses the current token in its car
			// and puts Nil in its cdr
			else if (tt2 == TokenType.RPAREN) {
				Console.WriteLine();
				return new Cons (parseExp(t1), nodeNil, cn++);
			}

			// Else if this token is a LPAREN, it is about to get complicated.
			else if (tt1 == TokenType.LPAREN) {

				// If this and the next token are a LPAREN-RPAREN pair,
				// return a Cons node whose car is Nil and cdr is the parsing of the rest of the list
				if (tt2 == TokenType.RPAREN) {
					return new Cons (
						nodeNil,
						parseRest(),
						cn++);
				}

				// If this token is a LPAREN and the next token is anything besides the RPAREN,
				// that means that this LPAREN is starting a new list of exps inside of an exp
				// and the next token is the start of the new inner list
				else {
					return new Cons (							// Make a Cons node
						new Cons(								// car
							parseExp(t2),							// car - the next token
							parseRest(),		// cdr - the parseRest of the next-next token
							cn++									//
						),
						parseRest (),		//cdr - the parseRest of the next-next-next token
						cn++
					);
				}
			}

			// At this point, we know that this token is an exp
			// and the next token is not a DOT or RPAREN or an inner list.
			// Therefore, we can infer that the next token must be an exp.
			// It then follows that we have to make
			// the car of this Cons node to be the parsing of the current token,
			// the cadr be the parsing of the next token,
			// and the cddr be the parsing of the rest of the list
			else {
				return new Cons(
				parseExp(t1),
				new Cons(
					parseExp(t2),
					parseRest(),
					cn++
				),
				cn++);
			}

		}

		// Parse the rest of the list
        protected Node parseRest() {
			Token t = scanner.getNextToken();
			return makeRest(t);
        }

		protected Node parseRest(Token t) {
			return makeRest(t);
		}

    }

}
