all:
	mcs SPP.cs Tokens/*.cs Parse/*.cs Tree/*.cs Special/*.cs

clean:
	@rm -f *~ */*~

parser:
	mcs Parse/Parser.cs Parse/Scanner.cs Tokens/*.cs Tree/*.cs Special/*.cs

veryclean:
	@rm -f SPP.exe *~ */*~
