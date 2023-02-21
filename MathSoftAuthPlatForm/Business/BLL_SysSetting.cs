using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Business
{
    public class BLL_SysSetting: BaseMathRoleAuthorEntities
    {
         
        private DbSet<SysSetting> contextItem = null;


        public BLL_SysSetting() : base()
        {
            contextItem = context.SysSettings;

        }

        public SysSetting GetSingle()
        {
            return contextItem.FirstOrDefault();
        }

        public UI_SysSetting GetStauts()
        {
            SysSetting modal = contextItem.FirstOrDefault();

            UI_SysSetting uI_SysSetting = new UI_SysSetting()
            {
                SysSettingId = modal.SysSettingId,
                SysBaoMingStatus = modal.SysBaoMingStatus
            };
            if (uI_SysSetting.SysBaoMingStatus == 0)
            {
                uI_SysSetting.tongzhaoStatus = false; uI_SysSetting.wunianStatus = false; uI_SysSetting.zhongzhuanStatus = false; uI_SysSetting.danzhaoStatus = false;
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

                uI_SysSetting.danzhaoStatus = DateTimeHelper.NowInArray(a1);
                uI_SysSetting.zhongzhuanStatus = DateTimeHelper.NowInArray(a2);
                uI_SysSetting.wunianStatus = DateTimeHelper.NowInArray(a4);
                uI_SysSetting.tongzhaoStatus = DateTimeHelper.NowInArray(a5);
            }
            return uI_SysSetting;
        }

        public UIModelData<SysSetting> Update(string WhoOp)
        {
            UIModelData<SysSetting> uIModelData = new UIModelData<SysSetting> { };
            try
            {
                SysSetting model = contextItem.FirstOrDefault();
                model.SysBaoMingStatus = model.SysBaoMingStatus == 0 ? 1 : 0;
                contextItem1.Add(new SysSetting_Log { SysSettingLogId = Guid.NewGuid(), InsertTime = DateTime.Now, OpResault = model.SysBaoMingStatus.Value.ToString(), WhoOp = WhoOp });
                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
                return uIModelData;
            }
            catch (Exception ex)
            {
                uIModelData.remark = ex.ToString();
                return uIModelData;
            }
        }
    }
}
