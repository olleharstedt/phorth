vocabulary php_args
php_args also
definitions
order cr
: string "string" type ;
previous Forth definitions
order cr

: <?php "<?php" type cr cr ;

: fn ( "foo" -- "function foo lparen rparen" )
    >in @ create >in ! here >r bl parse dup >r dup c, here swap cmove r> 1+ allot
    s" function " type r> count type s" (" type
    php_args also
    order cr
    does> count type s" ()" type ;

: returns 
    parse-name "): " type type cr "{" type cr 
    previous previous order cr
;

: endfn "}" type cr ;


\ : fn  ( "foo" -- "function foo lparen rparen" )
    \ >in @  create >in ! here bl parse s,
    \ "function " count type "()"
    \ does> count type "()" ;

\ 8 emit \ 8 is the back-delete button ascii char

<?php

fn foo
returns string
endfn

foo

cr
bye
