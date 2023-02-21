using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
   public class BLL_Math_ZZTARGET
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_ZZTARGET> contextItem = null;
        public BLL_Math_ZZTARGET()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_ZZTARGET;
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_ZZTARGET]", new object[] { });
        }

        public UIModelData<Math_ZZTARGET> Add(Guid Id,
       string zhanzhangdaqu
      , string school
      , string zhanzhang
          ,  Expression<Func<Math_ZZTARGET, bool>> perWhere)
        {
            Math_ZZTARGET model = new Math_ZZTARGET()
            {
                Id = Id,
                school = school,
                zhanzhang = zhanzhang,
                zhanzhangdaqu= zhanzhangdaqu

            };
            UIModelData<Math_ZZTARGET> uIModelData = new UIModelData<Math_ZZTARGET> { };
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


        public List<Math_ZZTARGET> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_ZZTARGET, bool>> where = null)
        {
            Expression<Func<Math_ZZTARGET, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_ZZTARGET, bool>> where = null)
        {
            Expression<Func<Math_ZZTARGET, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public void trans(gojsDiaModal cmd)
        {
            clearTable();
            List<gojsModal> list = cmd.nodeDataArray;
            List<gojsModal> OfGroups = list.Where(ite => ite.category == "OfGroups").ToList();
            foreach (gojsModal item in OfGroups)
            {
                List<gojsModal> maliang = GetList(list, item.key).Where(fg => fg.isGroup == false).ToList();

                foreach (gojsModal i in maliang)
                {
                    BLL_Math_ZZ bll = new BLL_Math_ZZ();
                    int total = 0;
                    Math_ZZ mk= bll.Search(-1, -1, out total, kk => kk.zhanzhangdaqu == item.text).FirstOrDefault();
                    string aaaa = "";
                    if (mk != null) {
                        aaaa = mk.zhanzhang;
                    }

                    Add(Guid.NewGuid(), item.text, i.text, aaaa, f => false);
                }
            }
            //马良写新的逻辑

        }

        List<gojsModal> GetList(List<gojsModal> source, string pid)
        {
            List<gojsModal> ret = new List<gojsModal>();
            List<gojsModal> temp = source.Where(item => item.group == pid).ToList();
            ret.AddRange(temp);

            foreach (gojsModal item in temp)
            {
                ret.AddRange(GetList(source, item.key));
            }
            return ret;
        }
    }
}
