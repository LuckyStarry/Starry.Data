# Starry.Data
[![Build Status](https://travis-ci.org/LuckyStarry/Starry.Data.svg)](https://travis-ci.org/LuckyStarry/Starry.Data)

## About Starry.Data
**Starry.Data** is a lightly database access component, you can write sql command text and execute it with DbClient.

Just like this:
```
var client = new Starry.Data.DbClient("your database's name on web/app.config");
var result = client.Query<type>(@"
    SELECT your column(s)
      FROM your table
");
```

## Copyright and license
Code and documentation copyright 2016 Sun Bo. Code released under [the MIT license](https://github.com/LuckyStarry/Starry.Data/blob/master/LICENSE).
