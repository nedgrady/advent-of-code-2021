grammar Monkey;

/*
 * Lexer Rules
 */
fragment INPUTSTART : '$';
fragment CD  : 'cd' ;
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
NONZERODIGIT: [1-9];
DIGIT: [0-9];

NUMBER: NONZERODIGIT DIGIT*;

WORD                : (LOWERCASE | UPPERCASE)+ ;
WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ ;
WHITEJUNK           : (NEWLINE | WHITESPACE)+ -> skip;
OPERATOR           : '+' | '*';



monkeyNumber: DIGIT;
monkeyProgram: monkeyDefinitions EOF;
monkeyDefinitions: monkeyDefinition+;
monkeyDefinition : 
    monkeyHeader
    monkeyStartingItems
    monkeyOperation
    monkeyTest (NEWLINE | EOF);

monkeyHeader: 'Monkey' monkeyNumber ':';

monkeyStartingItems: 'Starting items:' items;
items: item (',' item)*;
item: NUMBER;


monkeyOperation: 'Operation: new =' monkeyOperand OPERATOR monkeyOperand;
monkeyOperand: 'old' | NUMBER;

monkeyTest: monkeyTestCondition trueAction falseAction;
monkeyTestCondition: 'Test' ':' 'divisible' 'by' monkeyTestConditionDivisor;
monkeyTestConditionDivisor: NUMBER;
trueAction: 'If true: throw to monkey' monkeyNumber;
falseAction: 'If false: throw to monkey' monkeyNumber;
