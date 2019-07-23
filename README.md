# FOO CHESS SERVER

For MacOS development:
*  Install [brew](https://brew.sh)
*  Install [dotnet](https://dotnet.microsoft.com/download)

* Install postgresql
```
brew install postgresql
```


```
brew services start postgresql
```

 

```
psql -d postgres
```

 
```
create user chess_db_user password 'SomeAmazingPassword';
```

` \q ` to exit psql.

* Run EF migrations
```
dotnet ef database update
```