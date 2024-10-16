namespace DappeeDemo.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<bool> SaveData<T>(string spName, T parameters, string connectionId = "conn");

        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn");
    }
}