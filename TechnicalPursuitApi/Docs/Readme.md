### Commands

#### Install entity framework tools
```bash
> dotnet tool install --global dotnet-ef
```

#### Create migration with entity framework
```bash  
> dotnet ef migrations add InitialCreate -p .\Template.Infrastructure\ -s .\Template.Api\
```

#### Apply entity framework migrations
```bash  
> dotnet ef database update --project .\Template.Infrastructure\ --startup-project .\Template.Api\
```
## Docker

### SQL Server

#### Pull docker image
```bash
> docker pull mcr.microsoft.com/mssql/server:2022-latest 
```

#### Start SQL Server
```bash
> docker run -e 'HOMEBREW_NO_ENV_FILTERING=1' -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YOUR_PASSWORD>!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest 
```

> **Note**
> <YOUR_PASSWORD> should match the user password used in the for dbContext connection string


## AzureDevOps

### Build pipeline


### Release pipeline