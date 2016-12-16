# Starry.Data
[![Build Status](https://travis-ci.org/LuckyStarry/Starry.Data.svg)](https://travis-ci.org/LuckyStarry/Starry.Data)

## About Starry.Data
***Starry.Data*** is a lightly weight database access library, you can use the ***Starry.Data.DbClient*** for execute SQL commands. It will generate a connection for executing commands and you needn't worry how to dispose it because the connection would be released automatically after use.

## How to use
#### Get Started Fast

If you have a table ***BlogInfo*** in database ***blogdb*** like this:

| ID | Title | Content | CreateUser | CreateTime |
| :-: | :-: | :-: | :-: | :-: |
| 1 | Hello World | This is a test content | 1 | 2016-12-16 |
| 2 | Hello Starry | This is a test content too | 1 | 2016-12-16 |

Then you can query a blog list like this:
```C#
class BlogInfo
{
    public int ID { set; get; }
    public string Title { set; get; }
    public string Content { set; get; }
    public int CreateUser { set; get; }
    public DateTime CreateTime { set; get; }
}

var client = new Starry.Data.DbClient("blogdb");
var result = client.Query<BlogInfo>(@"
    SELECT *
      FROM BlogInfo
");
```

If you want to get the blogs written by user <1>, you can code like this:
```C#
var client = new Starry.Data.DbClient("sampledb");
var result = client.Query<BlogInfo>(@"
    SELECT *
      FROM BlogInfo
     WHERE CreateUser = @User
", new { User = 1 });
```

[Click here](https://github.com/LuckyStarry/Starry.Data/wiki/How-to-use) to view more.

## Copyright and license
Code and documentation copyright 2016 Sun Bo. Code released under [the MIT license](https://github.com/LuckyStarry/Starry.Data/blob/master/LICENSE).
