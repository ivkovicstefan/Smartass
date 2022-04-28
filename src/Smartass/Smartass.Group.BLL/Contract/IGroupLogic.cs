using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;

namespace Smartass.Group.BLL.Contract
{
    public interface IGroupLogic
    {
        ResponseDTO CreateGroup(GroupCreationDTO groupCreationDTO);

        ResponseDTO GetGroupListByUser(int userId);

        ResponseDTO SearchGroupListByUser(string searchText, int userId);

        ResponseDTO SendGroupInvite(GroupInvitationDTO groupInvitationDTO);
    }
}
