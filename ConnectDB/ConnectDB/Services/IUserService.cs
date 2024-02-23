using ConnectDB.Models;

namespace ConnectDB.Services
{
    public interface IUserService
    {
        List<Users> getAll();
        long save(Users user);
        List<Users> findOne(int id);
        void update(Users user, int id);
        void delete(int id);
    }
}
