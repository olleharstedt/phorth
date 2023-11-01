\ Create unaligned field in structure with given size
: +FIELD  OVER + SWAP CREATE , DOES> @ + ;

\ Create aligned cell field in structure
: FIELD:  ALIGNED  1 CELLS +FIELD ;

\ Structure example
0
    FIELD: SHAPE.WIDTH
    FIELD: SHAPE.HEIGHT
    FIELD: SHAPE.AREA
CONSTANT %SHAPE

\ Create a struct
CREATE SQUARE
        1 , 1 , 1 ,

\ Get height
SQUARE SHAPE.HEIGHT ?

\ Get area
SQUARE SHAPE.AREA ?

\ Set width
2 SQUARE SHAPE.WIDTH !
2 SQUARE SHAPE.AREA !
