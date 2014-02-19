

namespace IntegratedFlghtDynamicSystem.Global
{
    public interface IConfig
    {
        string ConnectionStrings(string connectionString);

        string Lang { get; }
    }
}
