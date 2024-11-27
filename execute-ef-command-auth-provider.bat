@echo off
set /p input= Execute ef command: 
set /p env= Enter environment (default: Local): 
if "%env%"=="" set env=Local
cd CQ.AuthProvider.DataAccess.EfCore
set ASPNETCORE_ENVIRONMENT=%env%
dotnet ef %input% --verbose --context AuthDbContext --startup-project ../CQ.AuthProvider.WebApi
pause
