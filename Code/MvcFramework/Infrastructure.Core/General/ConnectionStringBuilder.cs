using System.Data.EntityClient;
using System.Linq;

namespace Infrastructure.Core
{
    public static class ConnectionStringBuilder
    {
        public static string LocalMachine(string databaseName)
        {
                var entityBuilder = new EntityConnectionStringBuilder();
                entityBuilder.Provider = "System.Data.SqlClient";
                entityBuilder.ProviderConnectionString =
                    string.Format("data source=.;initial catalog={0};persist security info=True;user id=sa;password=d3hPOTraPC;multipleactiveresultsets=True;App={0}", databaseName);
                entityBuilder.Metadata = "res://*/Domain.csdl|res://*/Domain.ssdl|res://*/Domain.msl";
                return entityBuilder.ToString();
        }

    }
}
