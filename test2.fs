\ ---
\ Phorth library begins here
\ ---
vocabulary php_args \ The php_args word list is open only when writing down arguments to a function
php_args also definitions
\ order cr
: string "string" type ;
previous Forth definitions
\ order cr

: <?phorth "<?php" type cr cr ;

\ Generate function _and_ a word to call that function
: fn ( "foo" -- "function foo lparen rparen" )
    >in @ create >in ! here >r
    bl parse        \ Parse until next blank
    dup >r dup c, here swap cmove r> 1+ allot
    "function " type r> count type "(mixed $payload): mixed" type cr "{" type cr
    "$payload"
    \ php_args also
    \ order cr
    does> count type s" (...)," type cr
    ;

\ All functions must end with a return, which will return the variable on top of stack
\ (probably the payload)
: returns 
    parse-name "): " type type cr "{" type cr 
    previous previous
    \ order cr
    ;

\ Generate and IO class _and_ a word to invoke it
\ TODO: Effect type hierarchy
: io ( "foo" -- ? )
    \ Same code as for fn
    >in @ create >in ! here >r
    bl parse        \ Parse until next blank
    dup >r dup c, here swap cmove r> 1+ allot
    "class " type r> count type "{" type cr "}" type
    does> count type s" (...)," type cr
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

<?phorth

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

\ TODO: Separate vocabulary

\ warnings off
\ : io parse-name "new " type type "()," type cr ;
\ warnings on

: pipe "$result = pipe(" type cr ;

: end ")" type cr ;

: run "->run()" type cr ;

pipe
    htmlToMarkdown
    firstText
    end
    run

cr
bye
