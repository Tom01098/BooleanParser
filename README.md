# BooleanParser
[![NuGet Badge](https://buildstats.info/nuget/BooleanParser)](https://www.nuget.org/packages/BooleanParser)

A parser for evaluating boolean expressions. Similar to my [ArithmeticParser](https://github.com/Tom01098/ArithmeticParser), but using a backtracking approach to recursive descent instead of a predictive one.

## Usage
Create an instance of `BooleanParser.Parser` with the input boolean expression as a string. Then call the `BooleanParser.Parser.Parse` method, which will return the result of parsing, or throw a `BooleanParser.UnexpectedTokenException` if the input string could not be parsed. Note that the parser expects the input string to be in all caps - use the `string.ToUpper` method if needed. 

```C#
var parser = new Parser("TRUE AND FALSE");
var result = parser.Parse();
```

## Extended Backus-Naur Form Representation
The EBNF of the parser grammar is as follows.

```
Boolean := 'TRUE' | 'FALSE'

UnaryOperator := 'NOT'
BinaryOperator := 'AND' | 'OR' | 'XOR' | 'NOR' | 'NAND' | 'XNOR'

ParenthesisedExpression := '(' Expression ')'

Factor := Boolean | ParenthesisedExpression

Term := [UnaryOperator] Factor

Expression := Term {BinaryOperator Term}
```
