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
alter user isolani_db_user createdb;
```

` \q ` to exit psql.

* `SomeAmazingPassword`needs to be added to appsettings.json DB connection string
* Start application from the root folder of IsolaniServer
```
dotnet run
```

This should create the database, run EF migrations, and start the API.
