// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse {
	
    public class Scanner {
		
        private TextReader In;

        // Maximum length of strings and identifiers
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }

        public bool isWhiteSpace(char ch) {
			if ( ch ==  1 ||	// start of text
                 ch ==  9 ||	// horizontal tab
                 ch == 10 ||	// new line
                 ch == 11 ||	// vertical tab
                 ch == 12 ||	// form feed
                 ch == 13 ||	// carriage return
                 ch == 32		// space
               )
				return true;
            return false;
        }

        public bool isInitial(char ch) {
			if (isLetter(ch) ||
				isSpecialInitial(ch))
				return true;
            return false;
        }

        public bool isLetter(char ch) {
            if (ch >= 'A' && ch <= 'Z' ||
                ch >= 'a' && ch <= 'z' 	)
                 return true;
            return false;
        }

        public bool isSpecialInitial(char ch) {
            if (ch == '!' ||
				ch == '$' ||
				ch == '%' ||
				ch == '&' ||
				ch == '*' ||
                ch == '/' ||
				ch == ':' ||
				ch == '<' ||
				ch == '=' ||
				ch == '>' ||
                ch == '?' ||
				ch == '^' ||
				ch == '_' ||
				ch == '~'	)
				return true;
            return false;
        }

        public bool isSubsequent(char ch) {
            if (isInitial(ch)	||
				isDigit(ch)		||
				isSpecialSubsequent(ch))
                return true;
            return false;
        }

        public bool isDigit(char ch) {
            if (ch >= '0' && ch <= '9')
                return true;
            return false;
        }

        public bool isSpecialSubsequent(char ch) {
            if (ch == '+' ||
				ch == '-' ||
				ch == '.' ||
				ch == '@'	)
                return true;
            return false;
        }

        public bool isPeculiarIdent(char ch) {
            if (ch == '+' ||
				ch == '-')
				return true;
            return false;
        }

        public Token getNextToken() {
            
			char ch;

            try {
				// In.Read() returns int, so it is necessary to typecast it before storing it into ch
				ch = (char) In.Read();

				// Return null if the char read is -1
				// Or if it is the unknown char 65535 that Bobby encountered while debugging
				if ((int)ch == -1 ||
					(int)ch == 65535) {
                    return null;
                }

				// Skip white space by returning recursively calling getNextToken()
				// Which will eventually run into a token that it can return
				// Or into the end of file where it will then return null
                else if ( isWhiteSpace(ch) ) {
                    return getNextToken();
                }

                // Skip comments

				// Comments begin with a single semicolon and end with the start of a new line
                else if (ch == ';') {
					while (In.Peek() != 10) {	// While Peek() is not a new line
                        In.Read();           	// Skip it
                    }
                    return getNextToken();
                }

                // Special characters

				else if (ch == '\'' || (int)ch == 8216)
                    return new Token(TokenType.QUOTE);
				
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
				
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
				
                else if (ch == '.')
                    return new Token(TokenType.DOT);
				
				// We ignore the special identifier `...'.

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

				// String constants => Scan a string into the char array buf[] one char at a time
				// Strings begin and end with double quotes

				else if (	  ch == '"' ||
						 (int)ch == 8220  ) {
                    int count = 0;						// Initialize a counter to store items in the array
					while ((char)In.Peek() != '"' ||
						         In.Peek() != 8221) {	// If we aren't reading the end of the string
						buf[count] = (char)In.Read();	// Store next char into buf[]
                        count++;                		// And update the counter
                    }
                    In.Read();                  		// Read the next double quote and do nothing
					string s = new string(buf);			// Create a string from the char array buf[]
					buf = new char[BUFSIZE];			// Reinitialize buf[] because it will be used again for 
														// subsequent strings, integers, and identifiers
                    return new StringToken(s);
                }
					
                // Integer constants
				// Note: Make sure that the character following the integer is not removed from the input stream

                else if (isDigit(ch)) {
                    int count = 0;						// Initialize a counter to store items in the array
                    buf[count] = ch;                    // Save the first digit in buf[]
					while (isDigit((char)In.Peek())){	// While the next char is a digit,
						buf[count+1] = (char)In.Read(); // Store each subsequent digit of this integer to buf[]
                        count++;						// And update the counter
                    }
					int i = int.Parse(new string(buf)); // Convert buf[] to a string then to an int
					buf = new char[BUFSIZE];			// Reinitialize buf[] because it will be used again for
														// subsequent strings, integers, and identifiers 
					return new IntToken(i);
                }

                // Identifiers
				// Make sure that the character following the integer is not removed from the input stream

                else if (isInitial(ch) || isPeculiarIdent(ch)) {
                    if (isPeculiarIdent(ch))					// Recall peculiar identifiers are + and -
						return new IdentToken(""+ch);

                    else {
						int count = 0;      					// Initialize a counter to store items in the array
                        buf[count] = ch;   						// Store initial to buf[]
						while (isSubsequent((char)In.Peek())) {	// While the next char is a valid subsequent char
							buf[count+1] = (char)In.Read(); 	// Store it in buf[]
							count++;							// And update the counter
                        }
						string s = new string(buf).ToLower();	// Convert buf[] to a string and set all letters to 
																// lowercase because Scheme is not case-sensitive in 
																// regards to identifiers
						buf = new char[BUFSIZE];				// Reinitialize buf[] because it will be used again for 
																// subsequent strings, integers, and identifiers
                        return new IdentToken(s);
                    }
                }

                // Illegal character
				// Print the illegal char and its corresponding ASCII identifier to help the debugging process

				else {
					Console.Error.WriteLine("Illegal input character \'"
						+ (char)ch + " " + (int)ch + '\'');
					return getNextToken();
				}
            
			} catch (IOException e) {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }

        }

    }

}
