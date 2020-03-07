using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Service.Core
{
    public class CoreController : ControllerBase
    {
        #region Get Name Identifier
        private const string OBJECT_ID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public string GetObjectId
        {
            get
            {
                return User.Claims.First(c => c.Type.Equals(OBJECT_ID_CLAIM, StringComparison.InvariantCultureIgnoreCase)).Value;
            }
        }
        #endregion
    }
}
