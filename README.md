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
create user isolani_db_user password 'SomeAmazingPassword';
alter user isolani_db_user password createdb;
```

` \q ` to exit psql.

* Run EF migrations, from the root folder of IsolaniServer
```
dotnet ef database update
```
