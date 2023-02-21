using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DTOModel;

namespace Business
{
   public class BLL_Math_ZZ
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_ZZ> contextItem = null;
        public BLL_Math_ZZ()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_ZZ;
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_ZZ]", new object[] { });
        }

        public UIModelData<Math_ZZ> Add(string zhanzhang,
            string zhanzhangdaqu,
           string zhanzhangquyu,
            Expression<Func<Math_ZZ, bool>> perWhere)
        {
            Math_ZZ model = new Math_ZZ()
            {
                Id = Guid.NewGuid(),
                zhanzhang = zhanzhang,
                zhanzhangdaqu = zhanzhangdaqu,
                zhanzhangquyu = zhanzhangquyu,

            };
            UIModelData<Math_ZZ> uIModelData = new UIModelData<Math_ZZ> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("身份证号重复");
            }
            else
            {
                contextItem.Add(model);
                context.SaveChanges();
                uIModelData.status = 6;
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }


        public List<Math_ZZ> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_ZZ, bool>> where = null)
        {
            Expression<Func<Math_ZZ, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_ZZ, bool>> where = null)
        {
            Expression<Func<Math_ZZ, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }
        public Math_ZZ GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.Id == id);
        }

        public List<gojsModal> GetGojsList()
        {
            int total = 0;
            List<Math_ZZ> list = Search(-1, -1, out total, i => true);
            List<string> l1 = list.Select(item => item.zhanzhangdaqu).Distinct().ToList();

            List<gojsModal> ret = new List<gojsModal>();
            foreach (string item in l1)
            {
                string iiiid = Guid.NewGuid().ToString();
                ret.Add(new gojsModal() { category = "OfGroups", isGroup = true, key = iiiid, text = item , group=""});

                List<string> l2 = list.Where(j => j.zhanzhangdaqu == item).Select(df => df.zhanzhangquyu).ToList();

                foreach (string dfg in l2)
                {
                    ret.Add(new gojsModal() { category = "OfNodes", isGroup = true, key = Guid.NewGuid().ToString(), text = dfg, group = iiiid });
                }
            }

            BLL_Math_JYT  bLL_Math_JYT = new  BLL_Math_JYT ();
            List<string> l3= bLL_Math_JYT.Search(-1, -1, out total, i => true).Select(it=>it.GrandSchool).Distinct().ToList();
            foreach (string dfg in l3)
            {
                gojsModal fg = new gojsModal() { category = "", isGroup = false, key = Guid.NewGuid().ToString(), text = dfg, group = "" };
                gojsModal vb=  ret.FirstOrDefault(vvv=>vvv.category== "OfNodes"&&(dfg.Contains(vvv.text.Replace("县","").Replace("市",""))));

                if (vb != null)
                {
                    fg.group = vb.key;
                }

                ret.Add(fg);
            }
            return ret;

        }

    }
}
