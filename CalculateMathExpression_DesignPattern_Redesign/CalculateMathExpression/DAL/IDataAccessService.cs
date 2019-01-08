
using CalculateMathExpression.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateMathExpression.DAL
{
    public interface IDataAccessService
    {
        void Save(List<ButtonPermission> buttonPermissions);
        Task<List<ButtonPermission>> GetButtonPermissionAsync();
    }
}
