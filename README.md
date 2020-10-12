# ll-lolcode

An LLVM-backed LOLCODE compiler

# Purpose

It makes me laugh, and I wanted to play with LLVM.

# Status

Currently can run the following:

```
HAI

I SEZ "hello, world!"
```

```
HAI
    I HAZ A YARN ITZ BOB
    LOL BOB R ""hello, world!""
    I SEZ BOB
KTHXBYE
```

# Notes

* This is a strongly-typed dialect of LOLCODE.  Declare variables with `I HAZ A <NUMBR|NUMBAR|YARN> ITZ <identifier>`.
* Uses [Sprache](https://www.nuget.org/packages/Sprache/2.3.1) as the lexer-parser.