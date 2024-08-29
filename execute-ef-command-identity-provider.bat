@echo off
echo Execute ef command
set /p input= Command:
cd CQ.IdentityProvider.EfCore
dotnet ef %input% --verbose --context IdentityDbContext --startup-project ../CQ.AuthProvider.WebApi
pause