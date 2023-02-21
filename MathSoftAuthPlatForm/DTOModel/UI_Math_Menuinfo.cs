using System;

namespace DTOModel
{
    public class UI_Math_Menuinfo
    {
        public Guid MenuId { get; set; }
        public int MenuOrderId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuURL { get; set; }
        public string MenuRemark { get; set; }
        public string MenuIcon { get; set; }
        public DateTime MenuInsertTime { get; set; }
        public DateTime MenuUpdateTime { get; set; }

        public int? MenuOrderBy { get; set; }
    }
}
