using Zoo.Data;
using Zoo.Repository.IRepository;

namespace Zoo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IArrazaRepository Arraza { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Arraza = new ArrazaRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
