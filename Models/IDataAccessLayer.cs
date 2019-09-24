using System.Data;

namespace Filter.Models
{
    public interface IDataAccessLayer
    {
        bool CheckAddress(string address);
    }
}
