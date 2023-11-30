
\ https://wiki.laptop.org/go/Forth_Lesson_8
\ place  ( $ adr2 -- )           Save string at adr2 in counted form
\ pack   ( $ adr2 -- adr2 )      Like place but returns adr2
\ count  ( adr1 -- $ )           Convert counted string to string limits
\ $save  ( $1 adr2 -- $3 )       Save string to adr2 in counted form,
                               \ and return the string limits of the copy
\ $cat   ( $ adr2 -- )           Append $ to the counted string at adr2

\ 20:24 <+KipIngram> : here  dp @ ;
\ 20:24 <+KipIngram> : allot  dp +! ;

\ "asde" here place
\ here count type

\ Generate and IO class _and_ a word to invoke it
\ TODO: Effect type hierarchy
: io ( "foo" -- ? )
    \ Same code as for fn
    >in @ create >in ! here >r
    bl parse        \ Parse until next blank
    dup
    >r
    dup
    c,
    here
    swap
    cmove
    r>
    1+
    allot
    "class " type r> count type " {" type cr
    "  public function __invoke(mixed $payload): mixed" type cr
    "  {" type cr
    "    return " type cr
    "  }" type cr
    "}" type cr
    does>   \ What's on top of stack here?
    count type s" (...)," type cr
    ;

io file_get_contents
file_get_contents
