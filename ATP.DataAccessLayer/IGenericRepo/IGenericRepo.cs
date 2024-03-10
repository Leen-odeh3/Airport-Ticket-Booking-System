namespace ATP.DataAccessLayer.IGenericRepo;

public interface IGenericRepo<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
