namespace Zoo.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IArrazaRepository Arraza { get; }

        void Save();
    }
}
