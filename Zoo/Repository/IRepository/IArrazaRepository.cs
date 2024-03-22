using Zoo.Models;

namespace Zoo.Repository.IRepository
{
    public interface IArrazaRepository : IRepository<Arraza>
    {
        void Update(Arraza obj);
    }
}
