using Smartass.Group.BLL.Contract;
using Smartass.Group.DAL.Contract;

namespace Smartass.Group.BLL
{
    public class ScriptLogic : IScriptLogic
    {
        #region Private Fields

        private readonly IScriptDataAccess _scriptDataAccess;

        #endregion

        #region Constructor

        public ScriptLogic(IScriptDataAccess scriptDataAccess)
        {
            _scriptDataAccess = scriptDataAccess;
        }

        #endregion
    }
}
