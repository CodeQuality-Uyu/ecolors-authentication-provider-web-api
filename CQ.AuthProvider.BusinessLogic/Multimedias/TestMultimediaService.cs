
namespace CQ.AuthProvider.BusinessLogic.Multimedias;
internal sealed class TestMultimediaService
    : IMultimediaService
{
    public Multimedia GetById(Guid id)
    {
        return new Multimedia(id, "test", "test");
    }
}
