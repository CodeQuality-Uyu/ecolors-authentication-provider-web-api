# cq-auth-provider

## Configure Invalid Model Behavior
```C#
services
    .AddControllers(
    (options) =>
    {
        options.AddExceptionGlobalHandler();
    });
    .ConfigureApiBehaviorOptions(options =>
    {
       options.InvalidModelStateResponseFactory = context =>
       {
           var errors = context
           .ModelState
           .SelectMany(x => x.Value.Errors)
           .Select(x => x.ErrorMessage)
           .ToList();

           var errorResponse = new
           {
               InnerCode = "RequestInvalid",
               Message = "There is some problem with the request or with the values",
               Errors = errors
           };

           return new BadRequestObjectResult(errorResponse);
       };
    });
```

## Volumenes para base de datos no efimeras
```
version: '3.8'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:latest
    environment:
      SA_PASSWORD: "MySuperStrongPassword1(!)"
      MSSQL_USER: "sa"
      ACCEPT_EULA: "Y"
    ports:
      - "1430:1433"
    volumes:
      - sql_data:/var/opt/mssql 

  webapi:
    build: .
    image: cq-auth-provider-webapi
    ports:
      - "7190:80"
    environment:
      ASPNETCORE_ENVIRONMENT: "Docker"
      ASPNETCORE_HTTP_PORTS: 80
      ASPNETCORE_URLS: http://*:80
    depends_on:
      - sqlserver

volumes:
  sql_data:
```