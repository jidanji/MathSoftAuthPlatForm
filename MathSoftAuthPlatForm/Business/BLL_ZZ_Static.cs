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
    public class BLL_ZZ_Static: BaseMathRoleAuthorEntities
    {
        private DbSet<ZZ_Static> contextItem = null;
    
        public BLL_ZZ_Static():base()
        {
            contextItem = context.ZZ_Static;
        }

        public UIModelData<ZZ_Static> Add(ZZ_Static model, Expression<Func<ZZ_Static, bool>> perWhere)
        {
            UIModelData<ZZ_Static> uIModelData = new UIModelData<ZZ_Static> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("学生身份信息重复");
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


        public void Update(Guid Id
      , string ZhanZhangDaQu
      , string ZhanZhang)
        {

            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<ZZ_Static> contextItem = context.ZZ_Static;
            ZZ_Static model = contextItem.FirstOrDefault(item => item.Id == Id);
            model.ZhanZhangDaQu = ZhanZhangDaQu;
            model.ZhanZhang = ZhanZhang;
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<ZZ_Static> Search(int PageIndex, int PageSize, out int total, Expression<Func<ZZ_Static, bool>> where = null)
        {
            Expression<Func<ZZ_Static, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<ZZ_Static> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.ZhanZhangDaQu);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }


        public int GetTotal(Expression<Func<ZZ_Static, bool>> where = null)
        {
            Expression<Func<ZZ_Static, bool>> exp1 = where == null ? item => true : where;
            return contextItem.Count(exp1);
        }

        public ZZ_Static GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.Id == id);
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[ZZ_Static]", new object[] { });
        }

        public void Start()
        {
            //清理数据
            clearTable();

            BLL_View_AnalySISS bLL_View_AnalySISS = new BLL_View_AnalySISS();
            int total = 0;
            List<View_AnalySISS> list = bLL_View_AnalySISS.GetAll();
            foreach (View_AnalySISS item in list)
            {
                Add(new ZZ_Static()
                {
                    Id = Guid.NewGuid(),
                    StudentName = item.XSC_StudentName,
                    StudentIdCard = item.XSC_IdCard,
                    StudentType = item.tuijianren,
                    StudentSchool = item.JYT_GrandSchool,
                    ZhanZhangDaQuResource =( string.IsNullOrEmpty(item.Area)?"": item.Area).Replace(" ","").Replace("\t",""),
                }, i => i.StudentIdCard == item.XSC_IdCard);
            }

            List<ZZ_Static> zzlist = Search(-1, -1, out total, i => true);

            //非自然生源的
            var l1 = zzlist.Where(item => item.StudentType != "自然生源").ToList();
            foreach (ZZ_Static item in l1)
            {
                string ZhanZhangDaQuResource = item.ZhanZhangDaQuResource;
                BLL_Math_ZZ bLL_Math_ZZ = new BLL_Math_ZZ();

                List<Math_ZZ> listzz = bLL_Math_ZZ.Search(-1, -1, out total, 
                    i => ZhanZhangDaQuResource.Contains(i.zhanzhangquyu));

                if (listzz.Count > 0)
                {
                    Math_ZZ m1 = listzz.FirstOrDefault();
                    Update(item.Id, m1.zhanzhangdaqu, m1.zhanzhang);
                }
                ///没有匹配上那么我们走下非自然生源的逻辑
                else
                {
                    BLL_Math_ZZTARGET bLL_Math_ZZTARGET = new BLL_Math_ZZTARGET();
                    List<Math_ZZTARGET> zo = bLL_Math_ZZTARGET.Search(-1, -1, out total, i =>
                   item.StudentSchool.Contains(i.school)|| item.StudentSchool.Contains(i.zhanzhangdaqu));
                    if (zo.Count > 0)
                    {
                        Math_ZZTARGET mk = zo.FirstOrDefault();
                        Update(item.Id, mk.zhanzhangdaqu, mk.zhanzhang);
                    }
                }

            }

            //自然生源的
            var l2 = zzlist.Where(item => item.StudentType == "自然生源").ToList();

            foreach (ZZ_Static item in l2)
            {
                BLL_Math_ZZTARGET bLL_Math_ZZTARGET = new BLL_Math_ZZTARGET();
                List<Math_ZZTARGET> zo = bLL_Math_ZZTARGET.Search(-1, -1, out total,
                    i => item.StudentSchool.Contains(i.school) || item.StudentSchool.Contains(i.zhanzhangdaqu));
                if (zo.Count > 0)
                {
                    Math_ZZTARGET mk = zo.FirstOrDefault();
                    Update(item.Id, mk.zhanzhangdaqu, mk.zhanzhang);

                }
            }
        }

        public List<StaticUI> Static()
        {
            string sql2 = @"select base.*,t2.Math_Dept_Name from (
SELECT  count(*) Value,ZhanZhangDaQu,ZhanZhang
  FROM [dbo].[ZZ_Static]
  where 
  ZhanZhangDaQu	 is not null 
  and  ZhanZhang is not null 
  group by ZhanZhangDaQu,ZhanZhang
  ) base     left join dbo.Math_UserInfo t1 on base.ZhanZhang=t1.UserName
  left join dbo.Math_Deptinfo t2 on t1.User_Dept_Id=t2.Math_Dept_Id
  order by Math_Dept_Name,ZhanZhang";

            List <StaticUI> ret2 = context.Database.SqlQuery<StaticUI>(sql2, new List<object>().ToArray()).ToList();
            return ret2;

        }
    }
}
