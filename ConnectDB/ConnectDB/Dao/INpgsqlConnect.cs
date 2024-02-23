using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Npgsql;

namespace ConnectDB.DDL
{
    public interface INpgsqlDataSource
    {
        NpgsqlConnection connectionPostgres();
        void close();
        
    }
}
