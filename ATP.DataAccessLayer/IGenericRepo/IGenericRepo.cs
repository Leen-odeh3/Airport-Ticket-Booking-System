namespace ATP.BusinessLogicLayer.IGenericRepo;

public interface IGenericRepo<T>
{
    ICollection<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
