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
    does> count type s" (...)" type cr ;

: returns 
    parse-name "): " type type cr "{" type cr 
    previous previous
    \ order cr
;

: endfn "}" type cr ;

\ Push temporary variable on stack
: $t "$t" ;

: null "null" ;

\ Sets variable on top of stack to empty array [], leaves same variable on stack
( -- $t )
: [] "$t" 2dup type " = [];" type cr ;

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

( $x <name> -- $t )
: new parse-name "$t = " type "new " type type "(" type type ");" type cr "$t" ;

( $t -- )
: return "return " type type ";" type cr "}" type cr ;

( $t $obj -- )
: -> type parse-name "->" type type "(" type type ")" type ";" type cr "$t" ; 

\ : fn  ( "foo" -- "function foo lparen rparen" )
    \ >in @  create >in ! here bl parse s,
    \ "function " count type "()"
    \ does> count type "()" ;

\ 8 emit \ 8 is the back-delete button ascii char

( $length $offset $string -- $t )
: substr 
    "$t = substr(" type type ", " type . ", " type . ");" type cr "$t" ;

\ ---
\ Program begins here
\ ---

<?php

\ @param mixed $payload
\ @return mixed
fn htmlToMarkdown
    []                    \ Create new array for temporary variable $t
    "strip_tags" true =>  \ Access array, assuming it's top of stack
    new HtmlConverter     \ Eat $t on top of stack for constructor, and assign new $t
    -> convert            \ Call method 'convert' on top of stack, eat top of stack as argument to method, leave $t on top
    return                \ Return top of stack and end function

fn firstText
        \ $payload is on top of stack at function start
    50  \ length
    0   \ offset
    2swap   \ Put $payload at top
    substr 
    return

cr

warnings off
: new parse-name "new " type type "()" type ;
warnings on

: pipe "$result = pipe(" type cr ;

pipe
    htmlToMarkdown
    firstText
    new FileGetContents

cr
bye
