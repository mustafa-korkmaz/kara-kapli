using Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Dal.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly IMongoCollection<Entities.File> _files;

        public FileRepository(IOptions<AppSettings> appSettings)
        {
            var client = new MongoClient(appSettings.Value.MongoConfig.ConnectionString);
       
            var database = client.GetDatabase(appSettings.Value.MongoConfig.DatabaseName);

            _files = database.GetCollection<Entities.File>("files");
        }

        public Entities.File GetById(object id)
        {
          return _files.Find(file => file.Id == id.ToString()).FirstOrDefault();
        }

        public void Insert(Entities.File entity)
        {
            _files.InsertOne(entity);
        }
    }
}
