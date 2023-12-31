<?php

use Query\Factory;
use Query\ErrorLogLogger;
use Query\ParseHtml;
use Query\Effects\FileGetContents;
use League\HTMLToMarkdown\HtmlConverter;
use Query\Effects\Cache;
use function Query\pipe;

require __DIR__.'/vendor/autoload.php';
require __DIR__.'/src/functions.query.php';

//ini_set('user_agent','Mozilla/4.0 (compatible; MSIE 6.0)');
ini_set('user_agent','Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.13) Gecko/20080311 Firefox/2.0.0.13');
error_reporting(E_ALL);
ini_set("display_errors", true);

$urls = [
    "https://google.com",
    "https://bing.com",
    "https://duckduckgo.com",
    "https://search.yahoo.com"
];

$logger = new ErrorLogLogger();
$cache = new \Yiisoft\Cache\File\FileCache('/tmp/testfork');

/**
: htmlToMarkdown            \ $html argument is top of stack
    0 []                    \ Create empty array on top of stack
    "strip_tags" true =>    \ => adds strip_tags as key
    HtmlConverter new       \ Takes top of stack as constructor argument
    "convert" ->            \ -> calls method

Or

fn htmlToMarkdown
end

*/
function htmlToMarkdown(string $html): string
{
    $converter = new HtmlConverter(['strip_tags' => true]);
    return $converter->convert($html);
}

function firstText(string $text): string
{
    return substr($text, 0, 50);
}

$result = pipe(
    new Cache(new FileGetContents()),
    htmlToMarkdown(...),
    firstText(...)
)
    ->fork(2)
    ->setCache($cache)
    ->setLogger($logger)
    ->map($urls);

var_dump($result);
