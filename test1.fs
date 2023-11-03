s" tmp.php" w/o create-file throw Value fd-out

: startphp ( -- )
    s\" <?php\n" fd-out write-line
    ;
: w ( )
    fd-out write-file
    ;
: echo
    s\" echo \"" w drop
    w drop
    s\" \";\n" w
    ;
: endphp fd-out close-file throw ;

startphp
s" Hello world!\n" echo
endphp
bye
