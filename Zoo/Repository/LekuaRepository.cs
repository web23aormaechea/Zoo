using Zoo.Data;
using Zoo.Models;
using Zoo.Repository.IRepository;

namespace Zoo.Repository
{
    public class LekuaRepository : Repository<Lekua>, ILekuaRepository
    {
        private ApplicationDbContext _db;
        public LekuaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Lekua obj)
        {
            _db.Lekuak.Update(obj);
        }
    }
}
