// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }

        public bool isWhiteSpace(char ch) {
            if ( ch ==  1 || //start of text
                 ch ==  9 || //horizontal tab
                 ch == 10 || //new line
                 ch == 11 || //vertical tab
                 ch == 12 || //form feed
                 ch == 13 || //carriage return
                 ch == 32	 //space
               )
				return true;
            return false;
        }

        public bool isInitial(char ch) {
			if (isLetter(ch) || isSpecialInitial(ch))
				return true;
            return false;
        }

        public bool isLetter(char ch) {
            if (ch >= 'A' && ch <= 'Z' ||
                ch >= 'a' && ch <= 'z' )
                 return true;
            return false;
        }

        public bool isSpecialInitial(char ch) {
            if (ch == '!' || ch == '$' || ch == '%' || ch == '&' || ch == '*' ||
                ch == '/' || ch == ':' || ch == '<' || ch == '=' || ch == '>' ||
                ch == '?' || ch == '^' || ch == '_' || ch == '~')
                 return true;
            return false;
        }

        public bool isSubsequent(char ch) {
            if (isInitial(ch) || isDigit(ch) || isSpecialSubsequent(ch))
                return true;
            return false;
        }

        public bool isDigit(char ch) {
            if (ch >= '0' && ch <= '9')
                return true;
            return false;
        }

        public bool isSpecialSubsequent(char ch) {
            if (ch == '+' || ch == '-' || ch == '.' || ch == '@')
                return true;
            return false;
        }

        public bool isPeculiarIdent(char ch) {
            if (ch == '+' || ch == '-')
				return true;
            return false;
        }

        public Token getNextToken() {
            char ch;

            try
            {
				ch = (char) In.Read();

				if ((int)ch == -1) {
                    return null;
                }

                // Skip white space
                else if ( isWhiteSpace(ch) ) {
                    return getNextToken();
                }

                // Skip comments (comments begin with a single semicolon)
                else if (ch == ';') {
					while (In.Peek() != 10) { //while peek is not a new line
                        In.Read();            //do nothing to it--skip it
                    }
                    return getNextToken();
                }

                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);

                // Boolean constants
                else if (ch == '#') {
					ch = (char)In.Read();
                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
					else if ((int)ch == -1) {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants -> scan a string into the buffer variable buf
                else if (ch == '"') {
                    int count = 0;
					while ((char)In.Peek() != '"') {  // If we aren't reading the end of the string...
						buf[count] = (char)In.Read(); // update next item in buf array
                        count++;                // update counter
                    }
                    In.Read();                  // read the next double quote and do nothing
                                                // new String(char*, starting position, length of string)
                    return new StringToken(new String( buf, 0, count)); //count because length increases when count increases
                }
					
                // Integer constants
                else if (isDigit(ch)) {
                    int count = 0;
                    buf[count] = ch;                             // save the integer in a string
					while ((char)In.Peek() >= '0' && (char)In.Peek() <= '9'){// while the next char is a number
						buf[count+1] = (char)In.Read();           // append each proceeding digit of the integer to that string
                        count++;
                    }
					int i = int.Parse(new string(buf)); // convert char array to string to int
                                                                 // count+1 because when count=0, length=1
                    // make sure that the character following the integer
                    // is not removed from the input stream
					return new IntToken(i);
                }

                // Identifiers
                else if (isInitial(ch) || isPeculiarIdent(ch)) {
                    if (isPeculiarIdent(ch))
						return new IdentToken(""+ch);

                    else {
                        int count = 0;      // initialize a counter
                        buf[count] = ch;    // store initial to char array
						while (isSubsequent((char)In.Peek())) {
							buf[count+1] = (char)In.Read(); // update the buf array with subsequent chars of the identifier
                            count++;
                        }
                        return new IdentToken(new String(buf, 0, count+1).ToLower());
                        // to lower case because scheme is not case sensitive in regards to identifiers
                        // count+1 because when count=0, length=1
                    }
                    // make sure that the character following the integer
                    // is not removed from the input stream
                }

                // Illegal character
				else {
					Console.Error.WriteLine("Illegal input character '"
						+ (char)ch + '\'');
					return getNextToken();
				}
            
			}
            
            catch (IOException e) {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }

		public static int Main (string[] args) {
			Console.WriteLine ("Hello World!");
			return 1;
		}
    }

}
