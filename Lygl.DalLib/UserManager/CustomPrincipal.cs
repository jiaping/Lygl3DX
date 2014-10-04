using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Security;
using System.Security.Principal;
using Lygl.DalLib.UserManager;

namespace Lygl.DalLib.UserManager
{
    [Serializable()]
    public class CustomPrincipal : CslaPrincipal
    {
        private CustomPrincipal(IIdentity identity)
            : base(identity)
        { }

        public static bool Login(string username, string password)
        {
            var identity = CustomIdentity.GetIdentity(username, password);
            if (identity.IsAuthenticated)
            {
                CustomPrincipal principal = new CustomPrincipal(identity);
                Csla.ApplicationContext.User = principal;
                return true;
            }
            else
            {
                Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
                return false;
            }
        }

        public static void Logout()
        {
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }

        //public virtual bool IsInRole(string role)
        //{
        //    if (Identity != null)
        //        return Identity.IsInRole(role);
        //    else
        //        return false;
        //}
    }
}
