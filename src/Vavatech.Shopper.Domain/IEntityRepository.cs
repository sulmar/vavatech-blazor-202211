namespace Vavatech.Shopper.Domain
{
    // Interfejs generyczny (ugólniony) - szablon
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetById(int id);
        Task Update(TEntity entity);
    }
}
