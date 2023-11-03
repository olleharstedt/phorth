At a very conceptual stage.

The idea was to be able to benefit both from the Forth macro system and the huge PHP ecosystem.

Will probably code it in OCaml.

Stack docs would be mandatory.

The implicit stack should have configurable behaviour.

String interpolation should still work.

    "Something to show" echo

or

    ." Something to show with extra $content" echo

Hacks for `"`?

Compile mode. Interpret mode.

`[]` for arrays.

    [ 1 2 3 ]

Immediate words? Always executed, even if in compile mode.

VALUE, VARIABLE, CONSTANT

    : v+    ( a b c d -- a+c b+d)
        LOCALS| d c b a |
        a c +  b d +  ;

> CREATE makes a new dictionary entry.

Does not make sense without allocating memory to that entry.

    // CREATE x
    $x = new Word();

> 14:47 < KipIngram> But, in any case olle, create doesn't properly allocate space.

> HERE is a subroutine that returns the address of the next available space

CREATE variable, function, class, array in PHP?

What's postfix and what's prefix, and why? And how to create both types of words?

Execution token, xt:

> 14:16 < olle> hmmm `x <==> ' x

    CELLS
    ALLOT

Not sure these makes sense for a scripting lang.

> ALLOT increases the pointer HERE by the number of address units you have told it to allot

> Since THEN is used to terminate an IF statement rather than in its usual sense, some Forth writers prefer the name ENDIF. 

IF is compile-time only.


