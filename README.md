# BooleanParser
[![NuGet Badge](https://buildstats.info/nuget/BooleanParser)](https://www.nuget.org/packages/BooleanParser)

A parser for evaluating boolean expressions. Similar to my [ArithmeticParser](https://github.com/Tom01098/ArithmeticParser), but using a backtracking approach to recursive descent instead of a predictive one.

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
