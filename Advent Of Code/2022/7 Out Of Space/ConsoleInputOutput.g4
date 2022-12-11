grammar ConsoleInputOutput;
/*
 * Lexer Rules
 */
fragment INPUTSTART : '$';
fragment CD  : 'cd' ;
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
fragment SUBSEQUENTDIGIT : ('0'..'9');
fragment FIRSTDIGIT : ('1'..'9');

NUMBER: FIRSTDIGIT SUBSEQUENTDIGIT*;
WORD                : (LOWERCASE | UPPERCASE)+ ;
WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ ;


/*
 * Parser Rules
 */
extension :'.' WORD;
fileName : WORD;
fileSize: NUMBER;
fileDescriptor: fileName extension*;
directoryName : ('/' | WORD);
program: commands EOF;
commands : command*;
cdUp : '$ cd ..';
lsCommand : '$ ls';
fileSizeCommand : fileSize fileDescriptor;
dirCommand: 'dir' directoryName;
cdDown : '$ cd' directoryName;
cdCommand : (cdUp | cdDown);
command : (cdCommand | lsCommand | dirCommand | fileSizeCommand) NEWLINE*;





// commands            : command* ;
// directory           : WORD;
// change_directory    : CD directory;
// command             : INPUTSTART change_directory EOF ;
