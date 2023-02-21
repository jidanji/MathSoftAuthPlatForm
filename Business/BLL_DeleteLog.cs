using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_DeleteLog
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<DeleteLog> contextItem = null;

        public BLL_DeleteLog()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.DeleteLogs;
        }

        public void Add(DeleteLog model)
        {
            contextItem.Add(model);
            context.SaveChanges();
        }

    }
}
