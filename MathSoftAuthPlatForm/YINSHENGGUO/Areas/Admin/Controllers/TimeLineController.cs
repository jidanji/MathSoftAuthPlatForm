using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class TimeLineController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetData(int draw, int start, int length)
        {
            BLL_TimeLine bll = new BLL_TimeLine();
            int total = 0;

            List<TimeLine> list = bll.Search(start, length, out total, null);

            List<UI_TimeLine> uilist = new List<UI_TimeLine>();

            foreach (TimeLine item in list)
            {
                UI_TimeLine uI_Time = Mapper.Map<UI_TimeLine>(item);

                uI_Time.TimelineClientId = item.TimelineId.ToString();
                uI_Time.DateRange = new[] { uI_Time.BegTime, uI_Time.EndTime };
                uilist.Add(uI_Time);
            }
            UIRowsData<UI_TimeLine> datatablesModel = new UIRowsData<UI_TimeLine>()
            {
                remark = string.Empty,
                rows = uilist,
                status = 1,
                suc = true,
                total = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult SaveData(List<UI_TimeLine> list)
        {
            if (list == null) { list = new List<UI_TimeLine>(); }
            if (list.Where(item => item.DateRange == null).Count() > 0)
            {
                return new JsonResult()
                {
                    Data = new UIModelData<string>()
                    {
                        Data = "失败，不能有空的",
                        remark = "失败，不能有空的",
                        status = 0,
                        suc = false
                    }
                };
            }

            List<TimeLine> list1 = new List<TimeLine>();
            foreach (UI_TimeLine item in list)
            {
                TimeLine timeLine = Mapper.Map<TimeLine>(item);
                timeLine.BegTime = item.DateRange[0];
                timeLine.EndTime = item.DateRange[1];


                list1.Add(timeLine);
            }
            BLL_TimeLine bll = new BLL_TimeLine();
            var res = bll.Update(list1);


            List<UI_TimeLine> uilist = new List<UI_TimeLine>();

            foreach (TimeLine item in res)
            {
                UI_TimeLine uI_Time = Mapper.Map<UI_TimeLine>(item);

                uI_Time.TimelineClientId = item.TimelineId.ToString();
                uI_Time.DateRange = new[] { uI_Time.BegTime, uI_Time.EndTime };

                uilist.Add(uI_Time);
            }

            return new JsonResult()
            {
                Data = new UIRowsData<UI_TimeLine>()
                {
                    remark = string.Empty,
                    rows = uilist,
                    status = 1,
                    suc = true,
                    total = res.Count
                }
            };
        }
    }
}