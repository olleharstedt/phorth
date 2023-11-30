<?php

class file_get_contents
{
    public function __invoke()
    {
        echo 'here';
    }
}

$a = new file_get_contents();
$a();
