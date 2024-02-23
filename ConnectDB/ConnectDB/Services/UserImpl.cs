using ConnectDB.DDL;
using ConnectDB.Models;
using Npgsql;
using NpgsqlTypes;

namespace ConnectDB.Services
{
    public class UserImpl : NpgsqlData<Users> ,IUserService
    {
        public void delete(int id)
        {
            string sql = "DELETE FROM public.users WHERE id = " + id.ToString();
            query(sql);
        }

        public List<Users> findOne(int id)
        {
            string sql = "SELECT * FROM Users where users.id = "+ id.ToString();            
            return query(sql);
        }

        public List<Users> getAll()
        {

         string sql = "SELECT * FROM Users";
         return query(sql);

        }

        public long save(Users user)
        {
            string sql = "INSERT INTO Users (username, email, age) VALUES (@1, @2, @3 )";
            return insertOrUpdate(sql,user.username,user.email,user.age);
        }

        public void update(Users user, int id)
        {
            string sql = "UPDATE public.users SET username=@1, email=@2, age=@3 WHERE id = " + id;
            insertOrUpdate(sql, user.username, user.email, user.age);
        }
    }
}
