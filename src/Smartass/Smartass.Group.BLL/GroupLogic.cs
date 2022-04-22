using Smartass.Group.BLL.Contract;
using Smartass.Group.DAL.Contract;

namespace Smartass.Group.BLL
{
    public class GroupLogic : IGroupLogic
    {
        #region Private Fields

        private readonly IGroupDataAccess _groupDataAcess;

        #endregion

        #region Constructor
        public GroupLogic(IGroupDataAccess groupDataAcess)
        {
            _groupDataAcess = groupDataAcess;
        }

        #endregion
    }
}
