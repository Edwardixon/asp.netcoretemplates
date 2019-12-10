# Simple clean Arhitecture for writing REST APIs using dotnet core cli

## Recreate:
* Create the projects
``` 
dotnet new webapi -n API
dotnet new classlib -n Domain
dotnet new classlib -n Persistence
dotnet new classlib -n Application
```
* Create solution file
```
dotnet new sln
```
* Add projects to solution
```
dotnet sln add API/
dotnet sln add Domain/
dotnet sln add Persistence/
dotnet sln add Application/
```
* Add dependecies between projects
```
cd API
dotnet add reference ../Application/
cd .. 
cd Application
dotnet add reference ../Persistence/
dotnet add reference ../Domain/
cd .. 
cd Persistence 
dotnet add reference ../Domain/
cd..
dotnet restore
```
