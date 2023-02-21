using DataAccess;
using MathSoftModelLib;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DTOModel;
using MathSoftCommonLib;

namespace Business
{
    public class BLL_SysSetting
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<SysSetting> contextItem = null;
        public BLL_SysSetting()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.SysSettings;
        }

        public SysSetting GetSingle()
        {
            return contextItem.FirstOrDefault();
        }

        public UI_SysSetting GetStauts()
        {
            SysSetting modal = contextItem.FirstOrDefault();

            UI_SysSetting mmmm = new UI_SysSetting()
            {
                SysSettingId = modal.SysSettingId,
                SysBaoMingStatus = modal.SysBaoMingStatus
            };
            if (mmmm.SysBaoMingStatus == 0)
            {
                mmmm.tongzhaoStatus = false;
                mmmm.wunianStatus = false;
                mmmm.zhongzhuanStatus = false;
                mmmm.danzhaoStatus = false;
                mmmm.shengwaiStatus = false;
            }
            else
            {
                BLL_TimeLine bLL_TimeLine = new BLL_TimeLine();
                int total = 0;
                List<TimeLine> list = bLL_TimeLine.Search(-1, -1, out total, null);

                var a1 = list.Where(i => i.category == "单招").Select(i => new DateTimePare() { BegTime = i.BegTime, EndTime = i.EndTime }).ToList();
                var a2 = list.Where(i => i.category == "中专").Select(i => new DateTimePare() { BegTime = i.BegTime, EndTime = i.EndTime }).ToList();
                var a4 = list.Where(i => i.category == "五年一贯制").Select(i => new DateTimePare() { BegTime = i.BegTime, EndTime = i.EndTime }).ToList();
                var a5 = list.Where(i => i.category == "统招").Select(i => new DateTimePare() { BegTime = i.BegTime, EndTime = i.EndTime }).ToList();
                var a6 = list.Where(i => i.category == "省外招生录入").Select(i => new DateTimePare() { BegTime = i.BegTime, EndTime = i.EndTime }).ToList();
                mmmm.danzhaoStatus = DateTimeHelper.NowInArray(a1);
                mmmm.zhongzhuanStatus = DateTimeHelper.NowInArray(a2);
                mmmm.wunianStatus = DateTimeHelper.NowInArray(a4);
                mmmm.tongzhaoStatus = DateTimeHelper.NowInArray(a5);
                mmmm.shengwaiStatus = DateTimeHelper.NowInArray(a6);
            }
            return mmmm;
        }

        public UIModelData<SysSetting> Update()
        {
            UIModelData<SysSetting> uIModelData = new UIModelData<SysSetting> { };
            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<SysSetting> contextItem = context.SysSettings;
            SysSetting model = contextItem.FirstOrDefault();
            if (model.SysBaoMingStatus == 0)
            {
                model.SysBaoMingStatus = 1;
            }
            else
            {
                model.SysBaoMingStatus = 0;
            }
            context.SaveChanges();
            uIModelData.suc = true;
            uIModelData.Data = model;
            return uIModelData;
        }
    }
}
