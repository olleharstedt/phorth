<?php

// : counter:  create 0 ,  does> dup @ tuck 1+ swap ! ;
// counter: fred  ok
// fred
// <1> 140599575159712                  // After does>
// <2> 140599575159712 140599575159712  // After dup
// <2> 140599575159712 0                // After @
// <3> 0 140599575159712 0              // After tuck
// <3> 0 140599575159712 1              // After 1+
// <3> 0 1 140599575159712              // After swap
// <1> 0                                // After !

global $__STACK;
$__STACK = [];

function dup()
{
    error_log('dup');
    global $__STACK;
    $top = array_pop($__STACK);
    array_push($__STACK, $top);
    array_push($__STACK, $top);
}

function fetch()
{
    error_log('fetch');
    global $__STACK;
    $top = array_pop($__STACK);
    array_push($__STACK, $top->cell);
}

function tuck()
{
    error_log('tuck');
    global $__STACK;
    $top = array_pop($__STACK);
    $second = array_pop($__STACK);
    array_push($__STACK, $top);
    array_push($__STACK, $second);
    array_push($__STACK, $top);
}

function swap()
{
    error_log('swap');
    global $__STACK;
    $top = array_pop($__STACK);
    $second = array_pop($__STACK);
    array_push($__STACK, $top);
    array_push($__STACK, $second);
}

function store()
{
    error_log('store');
    global $__STACK;
    $address = array_pop($__STACK);
    $value   = array_pop($__STACK);
    $address->cell = $value;
}

function one_plus()
{
    error_log('one_plus');
    global $__STACK;
    $top = array_pop($__STACK);
    $top++;
    array_push($__STACK, $top);
}

class base
{
    public function __invoke()
    {
        error_log('base::__invoke');
        global $__STACK;
        array_push($__STACK, $this);
    }
}

class counter extends base
{
    public $cell = 0;
    public function __invoke()
    {
        parent::__invoke();
        error_log('counter::__invoke');
        global $__STACK;
        dup();
        fetch();
        tuck();
        one_plus();
        swap();
        store();
    }
}

$fred = new counter();
$fred();
$fred();
$fred();
var_dump($__STACK);
