
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public interface IInvitationService
{
    Task CreateAsync(CreateInvitationArgs args);
}
