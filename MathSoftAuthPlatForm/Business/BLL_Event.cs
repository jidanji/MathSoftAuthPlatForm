using DTOModel;
using System;
using System.Collections.Generic;

namespace Business
{
    public class BLL_Event
    {
        public List<UIEventInfo> GetData()
        {
            List<UIEventInfo> list = new List<UIEventInfo>()
            {
                 new UIEventInfo {   BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张明去衡水一中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="王华去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张冰去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {   BegDateStr="2018-1-21",EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张明去衡水一中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo { BegDateStr="2018-1-21",  EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="王华去郑口二中宣讲出差"},
                 new UIEventInfo {   BegDateStr="2018-1-21",EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张冰去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo {  BegDateStr="2018-1-21", EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo { BegDateStr="2018-1-21",  EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo { BegDateStr="2018-1-21",  EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
                 new UIEventInfo { BegDateStr="2018-1-21",  EventId= Guid.NewGuid(), EventStatusId=1, EventStatusTitle="待执行", EventTitle="张美美去郑口二中宣讲出差"},
            };

            return list;
        }
    }
}
