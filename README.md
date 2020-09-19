# Workout Planner
REST API created using .NET Core framework. It allows you to store different items and edit them as you wish. You must register before getting access to the store. Authorization is based on JWT tokens. Data is stored using MSSQL

## Technologies used
* C#
* .NET Core
* MSSQL Database
* JWT

## Running on docker
****
**_Make sure you created Dockerfile and docker-compose.yml using example template file with corresponding variables_**

****
```
git clone https://github.com/PiotrBlachnio/Store-API.git
```

```
cd Store-API/
```

```
docker-compose build
```

```
docker-compose up
```
## Running on localhost
****
**_Make sure you added your database connection string in appsettings.json file_**

****

```
git clone https://github.com/PiotrBlachnio/Store-API.git
```

```
cd Store-API/
```

```
dotnet build
```

```
dotnet run
```

## Contributing
1. Fork it (https://github.com/PiotrBlachnio/Store-API/fork)
1. Create your feature branch (git checkout -b feature/fooBar)
1. Commit your changes (git commit -am 'Add some fooBar')
1. Push to the branch (git push origin feature/fooBar)
1. Create a new Pull Request