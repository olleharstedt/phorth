\ ---
\ Phorth library begins here
\ ---
vocabulary php_args \ The php_args word list is open only when writing down arguments to a function
php_args also definitions
\ order cr
: string "string" type ;
previous Forth definitions
\ order cr

: <?php "<?php" type cr cr ;

: fn ( "foo" -- "function foo lparen rparen" )
    >in @ create >in ! here >r bl parse dup >r dup c, here swap cmove r> 1+ allot
    "function " type r> count type "(mixed $payload): mixed" type cr "{" type cr
    "$payload"
    \ php_args also
    \ order cr
    does> count type s" ()" type ;

: returns 
    parse-name "): " type type cr "{" type cr 
    previous previous
    \ order cr
;

: endfn "}" type cr ;

\ Push temporary variable on stack
: $t "$t" ;

\ Sets variable on top of stack to empty array [], leaves same variable on stack
( $t -- $t )
: [] 2dup type " = [];" type cr ;

warnings off
( -- )
: true "true" ;
warnings on

( $t key value -- $t )
: => 
    2rot      \ Put $t on top of stack
    2dup 2>r  \ Put a copy of $t on r
    type      \ Put $t on top of stack and print it
    "['" type
    2swap type   \ Put key on top of stack
    "'] = " type
    \ value is top of stack
    type ";" type cr
    2r>       \ Put $t back on stack
;

( $x <name> -- )
: new parse-name "new " type type "(" type type ")" type cr ;

( -- )
: return "return " type ;

\ : fn  ( "foo" -- "function foo lparen rparen" )
    \ >in @  create >in ! here bl parse s,
    \ "function " count type "()"
    \ does> count type "()" ;

\ 8 emit \ 8 is the back-delete button ascii char

\ ---
\ Program begins here
\ ---

<?php

fn foo
    $t []
    "strip_tags" true =>
    new HtmlConverter
\ TODO: string $html or assume $payload, always only one argument and always `mixed`?
                        \ $payload is on stack
\ []                    \ Write array to temporary variable $t, put it on stack
\ "strip_tags" true =>  \ Use variable on stack
\ new HtmlConverter     \ Use variable on stack as input to constructor, set variable to $t
\ -> convert            \ Call method convert on variable on stack, use next item on stack as input
endfn

foo

cr
bye
