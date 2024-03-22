using Zoo.Models;

namespace Zoo.Repository.IRepository
{
    public interface ILekuaRepository : IRepository<Lekua>
    {
        void Update(Lekua obj);
        void Save();
    }
}
