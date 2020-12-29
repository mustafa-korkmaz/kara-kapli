
namespace Dal.Repositories.File
{
    public interface IFileRepository //todo implement IRepository<Entities.File>
    {
        Entities.File GetById(object id);
        void Insert(Entities.File entity);
    }
}
