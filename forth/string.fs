: $@len ( addr -- u ) @ @ ;
: delete ( addr u n -- )
  over min >r r@ - ( left over ) dup 0>
  IF 2dup swap dup r@ + -rot swap move THEN + r> bl fill ;
