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

		// Boolean helper-functions to recognize tokens and their types
		// Many of these are tests to qualify identifiers

        public bool isWhiteSpace(char ch) {
			if ( ch ==  1 ||	// Start of text
                 ch ==  9 ||	// Horizontal tab
                 ch == 10 ||	// New line
                 ch == 11 ||	// Vertical tab
                 ch == 12 ||	// Form feed
                 ch == 13 ||	// Carriage return
                 ch == 32		// Space
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
            if ((ch >= 'A' && ch <= 'Z') ||
                (ch >= 'a' && ch <= 'z'))
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

		// Take in characters from input one at a time and make appropriate
		// tokens out of them

        public Token getNextToken() {
			char ch;
            try {
				// In.Read() returns int, so it is necessary to typecast it
				// before storing it into ch
				ch = (char) In.Read();

				// Return null if the char read is the integer -1
				// Or if it is the unknown char 65535 that Bobby encountered
				// while debugging
				if ((int)ch == -1 ||
					(int)ch == 65535) {
                    return null;
                }

				// Skip white space by returning recursively calling
				// getNextToken() which will eventually run into a token that it
				// can return or into the end of file where it will then
				// return null
                else if ( isWhiteSpace(ch) ) {
                    return getNextToken();
                }

                // Skip comments. Comments begin with a single semicolon
				// and end with the start of a new line
                else if (ch == ';') {
					while (In.Peek() != 10){// While Peek() is not a new line
                        In.Read();       	// Skip it
					}
                    return getNextToken();
                }

                // Special characters

				// Char 8216 is the accented single quote
				else if (ch == '\'' || (int)ch == 8216)
                    return new Token(TokenType.QUOTE);

                else if (ch == '(')
                    return new Token(TokenType.LPAREN);

                else if (ch == ')')
                    return new Token(TokenType.RPAREN);

                else if (ch == '.')
                    return new Token(TokenType.DOT);

				// We ignore the special identifier '...'

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

				// String constants => Scan a string into the char array buf[]
				// one char at a time
				// Strings begin and end with double quotes

				// Chars 8220 and 8221 are accented double quotes
				else if (ch == '\"' ||
					(int)ch == 8220 ||
					(int)ch == 8221) {

					// Initialize a local counter to store items in the array
                	int count = 0;

					// If we aren't reading the end of the string
					while ((char)In.Peek() != '\"' ||
								 In.Peek() == 8220 ||
								 In.Peek() == 8221) {
						// Store next char into buf[] and update the counter
						buf[count] = (char)In.Read();
                        count++;
                    }

					// Read the next double quote and do nothing
                    In.Read();

					// Create a string from the char array buf[]
					string s = new string(buf);

					// Reinitialize buf[] because it will be used again for
					// subsequent strings, integers, and identifiers
					buf = new char[BUFSIZE];

					// Return a token containing the string that we created
                    return new StringToken(s);
                }

                // Integer constants
				// Note: Make sure that the character following the integer is
				// not removed from the input stream

                else if (isDigit(ch)) {
					// Initialize a local counter to store items in buf[]
					int count = 0;

					// Save the first digit in buf[]
                    buf[count] = ch;

					// While the next char is a digit,
					// Store that subsequent digit of this integer to buf[]
					// And update the counter
					while (isDigit((char)In.Peek())) {
						buf[count+1] = (char)In.Read();
                        count++;
                    }

					// Convert buf[] to a string then to an integer
					int i = int.Parse(new string(buf));

					// Reinitialize buf[] because it will be used again for
					// subsequent strings, integers, and identifiers
					buf = new char[BUFSIZE];

					// Return a token containing the integer we created
					return new IntToken(i);
                }

                // Identifiers
				// Make sure that the character following the integer is not
				// removed from the input stream

                else if (isInitial(ch) || isPeculiarIdent(ch)) {
					// Recall that peculiar identifiers are '+' and '-'
                    if (isPeculiarIdent(ch))
						return new IdentToken(""+ch);

                    else {
						// Initialize a local counter to store items in buf[]
						int count = 0;

						// Store Initial to buf[]
                        buf[count] = ch;

						// While the next char is a valid subsequent char
						// Store it in buf[]
						// And update the counter
						while (isSubsequent((char)In.Peek())) {
							buf[count+1] = (char)In.Read();
							count++;
                        }

						// Convert buf[] to a string and set all letters to
						// lowercase because Scheme is not case-sensitive in
						// regards to identifiers
						string name = new string(buf).ToLower();

						// Reinitialize buf[] because it will be used again for
						// subsequent strings, integers, and identifiers
						buf = new char[BUFSIZE];

						// Return a token with the name of the identifier
                        return new IdentToken(s);
                    }
                }

                // Illegal character
				// Print the illegal char and its corresponding ASCII
				// integer to help the debugging process

				else {
					Console.Error.WriteLine("Illegal input character \'"
						+ (char)ch + "\' " + (int)ch);
					return getNextToken();
				}

			} catch (IOException e) {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }

        }

    }

}
