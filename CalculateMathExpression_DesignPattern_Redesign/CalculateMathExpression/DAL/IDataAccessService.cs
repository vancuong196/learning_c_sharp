
using CalculateMathExpression.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateMathExpression.DAL
{
    public interface IDataAccessService
    {
        void Save(Dictionary<string,bool> buttonPermissions);
        Task<Dictionary<string,bool>> GetButtonPermissionAsync();
    }
}
