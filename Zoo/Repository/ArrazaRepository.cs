using System.Linq.Expressions;
using Zoo.Data;
using Zoo.Models;
using Zoo.Repository.IRepository;

namespace Zoo.Repository
{
    public class ArrazaRepository : Repository<Arraza>, IArrazaRepository 
    {
        private ApplicationDbContext _db;
        public ArrazaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Arraza obj)
        {
            _db.Arrazak.Update(obj);
        }
    }
}
