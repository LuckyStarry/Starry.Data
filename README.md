# Starry.Data
[![Build Status](https://travis-ci.org/LuckyStarry/Starry.Data.svg)](https://travis-ci.org/LuckyStarry/Starry.Data)

## About Starry.Data
**Starry.Data** is a lightly database access component, you can use the *Starry.Data.DbClient* for execute SQL commands. It will generate a connection for executing commands and you needn't worry how to dispose it because the connection would be released automatically after use.

For example, you can query a recordset like this:
```
class Entity
{
    //some public properties
}

var client = new Starry.Data.DbClient("your database's name on web/app.config");
var result = client.Query<Entity>(@"
    SELECT your column(s)
      FROM your table
");
```

## Copyright and license
Code and documentation copyright 2016 Sun Bo. Code released under [the MIT license](https://github.com/LuckyStarry/Starry.Data/blob/master/LICENSE).
