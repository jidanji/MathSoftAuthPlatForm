using System;

namespace DTOModel
{
    public class UIEventInfo
    {
        public Guid EventId { get; set; }

        public string EventTitle { get; set; }

        public int EventStatusId { get; set; }

        public string EventStatusTitle { get; set; }

        public DateTime BegDate { get; set; }

        public String BegDateStr { get; set; }
    }
}
