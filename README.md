# Recording-Customer-Data

Basic Rest API for recording customer data in C# .NET Core

## Steps

- .Net Core 5.0 SDK
- SQLServer Express

```
git clone https://github.com/EvertonGavioli/recording-customer-data.git

cd recording-customer-data

git checkout feature/Everton_Pereira_Gavioli

dotnet restore

cd src\RCD.Application\RCD.API

dotnet ef database update

dotnet run

https://localhost:5001/swagger/index.html

```

## Using Insomnia

```
import file from root folder "Insomnia_xxxx-xx-xx.json"
```
