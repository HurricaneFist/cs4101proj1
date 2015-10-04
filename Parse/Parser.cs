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
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse {

	public class Parser {
	
        private Scanner scanner;

		public Node nodeFalse = new BoolLit(false);
		public Node nodeTrue  = new BoolLit(true);
		public Node nodeNil   = new Nil();

        public Parser(Scanner s) { scanner = s; }

		public Node nilNode(){
			Console.WriteLine ("()");
			return nodeNil;
		}

		public Node makeNode (Token t) {
			TokenType tt = t.getType();

			if (tt == TokenType.LPAREN) {
				Console.WriteLine ("LPAREN");
				return parseRest(scanner.getNextToken());
			}

			else if (tt == TokenType.FALSE) {
				Console.WriteLine ("FALSE : #f");
				return nodeFalse;
			}

			else if (tt == TokenType.TRUE) {
				Console.WriteLine ("TRUE : #t");
				return nodeTrue;
			}

			else if (tt == TokenType.QUOTE) {
				Console.WriteLine ("QUOTE : \'");
				return parseExp();
			}

			else if (tt == TokenType.INT) {
				Console.WriteLine ("INT : " + t.getIntVal());
				return new IntLit(t.getIntVal());
			}

			else if (tt == TokenType.STRING) {
				Console.WriteLine ("STRING : " + t.getStringVal());
				return new StringLit(t.getStringVal());
			}

			else {//(tt == TokenType.IDENT)
				Console.WriteLine ("IDENT : " + t.getName());
				return new Ident(t.getName());
			}
		}

        public Node parseExp() {
			Token t = scanner.getNextToken();
			return makeNode (t);
        }

		public Node parseExp(Token t) {
			return makeNode (t);
		}
  		
		//    rest -> )
		//         |  exp+ [. exp] )
        protected Node parseRest(Token t) {
			

			if (t.getType() == TokenType.RPAREN) {
				Console.WriteLine ("NIL : ()");
				return nodeNil;
			}

			else {
				Token t2 = scanner.getNextToken ();
				TokenType tt2 = t2.getType();
				Console.WriteLine ("CONS");

				if (tt2 == TokenType.RPAREN) {
					return new Cons(parseExp(t), nilNode());
				}

				else if (tt2 == TokenType.DOT) {
					return new Cons(parseExp(t), parseExp(t2));
				}

				else { // if (t2 == exp)
					return new Cons(parseExp(t), parseRest(t2));
				}
			}
        }

		public static int Main (string[] args) {
			
			Scanner scanner = new Scanner (Console.In);
			Parser parser = new Parser (scanner);
			Node root;
			root = parser.parseExp ();
			/*
			while (tok != null) {
				//TokenType tt = tok.getType();
				//Console.Write(tt + " ");

				if (tt == TokenType.INT)
					Console.WriteLine(", intVal = " + tok.getIntVal());
				else if (tt == TokenType.STRING)
					Console.WriteLine(", stringVal = " + tok.getStringVal());
				else if (tt == TokenType.IDENT)
					Console.WriteLine(", name = " + tok.getName());
				else
					Console.WriteLine();
				
				tok = scanner.getNextToken();
			}
			*/

			return 0;
		}

    }
}
