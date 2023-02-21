using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using DTOModel;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_Student
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_Student> contextItem = null;
        public BLL_Math_Student()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_Student;
        }

        public UIModelData<Math_Student> Add(
string StudentName
,string StudentIDCard
, string StudentPhone
, string StudentType
, string StudentSchool
, Guid UserId
, Guid StudentZhuanYeId
,string StudentAddress
,string Remark
,string Sex
,string Area
,string UserName
,string UserPhone
,Guid Math_Dept_Id
,string  Math_Dept_Name
,Expression<Func<Math_Student, bool>> preWhere)
        {
            Math_Student model = new Math_Student()
            {
                StudentIDCard = StudentIDCard,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                StudentPhone = StudentPhone,
                StudentType = StudentType,
                StudentSchool = StudentSchool,
                UserId = UserId,
                StudentZhuanYeId = StudentZhuanYeId,
                StudentId = Guid.NewGuid(),
                StudentName = StudentName,
                StudentAddress = StudentAddress,
                Remark = Remark,
                UserDeptId = Math_Dept_Id,
                UserDeptName = Math_Dept_Name,
                UserName = UserName,
                UserPhone = UserPhone,
                Sex = Sex,
                Area = Area
            };
            UIModelData<Math_Student> uIModelData = new UIModelData<Math_Student> { };

            UI_SysSetting uI_SysSetting=   new BLL_SysSetting().GetStauts();
            if (uI_SysSetting.SysBaoMingStatus == null || uI_SysSetting.SysBaoMingStatus == 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("报名状态已经关闭");
                return uIModelData;
            }
            if (StudentType == "单招")
            {
                if (!uI_SysSetting.danzhaoStatus)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("报名状态已经关闭");
                    return uIModelData;
                }
            }
            if (StudentType == "统招")
            {
                if (!uI_SysSetting.tongzhaoStatus)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("报名状态已经关闭");
                    return uIModelData;
                }
            }
            if (StudentType == "中专") {
                if (!uI_SysSetting.zhongzhuanStatus)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("报名状态已经关闭");
                    return uIModelData;
                }
            }
            if (StudentType == "五年一贯制")
            {
                if (!uI_SysSetting.wunianStatus)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("报名状态已经关闭");
                    return uIModelData;
                }
            }
            try
            {
                int total = GetTotal(preWhere);
                if (total > 0)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("【身份证号】重复");
                }
                else if (StudentIDCard.Length != 18)
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("【身份证号】必须为18位，输入格式错误");
                }
                else if (string.IsNullOrEmpty(StudentType))
                {
                    uIModelData.status = 7;
                    uIModelData.suc = false;
                    uIModelData.remark = string.Format("【学生性质】不能为空");
                }
                else
                {
                    BLL_Math_JYT bLL_Math_JYT = new BLL_Math_JYT();
                    int jytTotal= bLL_Math_JYT.GetTotal(i => i.IdCard == StudentIDCard);
                    if (jytTotal > 0)
                    {
                        uIModelData.status = 7;
                        uIModelData.suc = false;
                        uIModelData.remark = string.Format("此人已经被录取，录入失败");
                    }

                    else
                    {
                        int number = Convert.ToInt32((StudentIDCard[16])) % 2;
                        model.Sex = number == 1 ? "男" : "女";
                        contextItem.Add(model);
                        context.SaveChanges();
                        uIModelData.status = 6;
                        uIModelData.suc = true;
                        uIModelData.Data = model;
                    }


                   
                }
            }
            catch (Exception ex)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("发生错误" + ex.Message);
            }
            return uIModelData;
        }

        public void GengZhengStudent(Guid studentId, string dingZhengName, string dingZhengNumber)
        {
            Math_Student model = contextItem.FirstOrDefault(item => item.StudentId == studentId);
            model.DingZhengName = dingZhengName;
            model.DingZhengNumber = dingZhengNumber;
            model.DingZhengUpdateTime = DateTime.Now;
            context.SaveChanges();
        }

        public UIModelData<Math_Student> Update(
Guid StudentId
, string StudentIDCard
, string StudentPhone
, string StudentType
, string StudentSchool
, Guid StudentZhuanYeId
, Expression<Func<Math_Student, bool>> perWhere)
        {
            UIModelData<Math_Student> uIModelData = new UIModelData<Math_Student> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("身份证号或者手机号重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_Student> contextItem = context.Math_Student;

                Math_Student model = contextItem.FirstOrDefault(item => item.StudentId == StudentId);

                model.StudentIDCard = StudentIDCard;
                model.StudentPhone = StudentPhone;
                model.StudentType = StudentType;
                model.StudentSchool = StudentSchool; ;
                model.StudentZhuanYeId = StudentZhuanYeId;



                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public Math_Student Delete(Guid id)
        {
            Math_Student model = contextItem.FirstOrDefault(item => item.StudentId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
            return model;

        }

        public List<Math_Student> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_Student, bool>> where = null)
        {
            Expression<Func<Math_Student, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.StudentOrderId).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.StudentOrderId).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_Student, bool>> where = null)
        {
            Expression<Func<Math_Student, bool>> exp1 = where == null ? item => true : where;
            return contextItem.Count(exp1);
        }

        public Math_Student GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.StudentId == id);
        }
    }
}
