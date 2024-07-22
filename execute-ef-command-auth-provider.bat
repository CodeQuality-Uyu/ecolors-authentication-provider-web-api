@echo off
echo Execute ef command
set /p input= Command:
cd CQ.AuthProvider.DataAccess.EfCore
dotnet ef %input% --verbose --context AuthDbContext --startup-project ../CQ.AuthProvider.WebApi
pause