using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;

namespace Smartass.Group.DAL.Contract
{
    public interface IGroupDataAccess
    {
        ResponseDTO CreateGroup(GroupCreationDTO groupCreationDTO);
        ResponseDTO GetGroupListByUser(int userId);
        ResponseDTO SearchGroupListByUser(string searchText, int userId);
    }
}
