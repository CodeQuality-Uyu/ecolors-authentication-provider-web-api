namespace CQ.AuthProvider.BusinessLogic.Licenses;
public interface ILicenseRepository
{
    Task CreateAsync(License license);
}
