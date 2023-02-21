using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_Math_User_Role_Select : BaseMathRoleAuthorEntities
    {

        private DbSet<Math_User_Role_Select> contextItem = null;

        public BLL_Math_User_Role_Select() : base()
        {
            contextItem = context.Math_User_Role_Select;
        }

        public void DeleteRelationShipByUserId(Guid UserId)
        {
            IQueryable<Math_User_Role_Select> query = contextItem.Where(item => item.UserId == UserId);
            if (query != null)
            {
                contextItem.RemoveRange(query);
                context.SaveChanges();
            }
          
        }
    }
}
