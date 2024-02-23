using Npgsql;

namespace ConnectDB.Repository
{
    public interface INpgsqlTransaction
    {

        NpgsqlTransaction benginTransaction();
        void commit(); 
        void rollback(); 


    }
}
