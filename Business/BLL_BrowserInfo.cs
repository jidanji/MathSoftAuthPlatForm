using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_BrowserInfo
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<BrowserInfo> contextItem = null;

        public BLL_BrowserInfo()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.BrowserInfoes;
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="model"></param>
        public void Add(string BrowserName, string BrowserVersion, string UserName, string BrowserType, string Platform, string Frames, string Tables, string Cookies)
        {
            BrowserInfo browserInfo = new BrowserInfo()
            {
                BrowserId = Guid.NewGuid(),
                BrowserName = BrowserName,
                BrowserVersion = BrowserVersion,
                UserName = UserName,
                BrowserType = BrowserType,
                Cookies = Cookies,
                Frames = Frames,
                InsertTime = DateTime.Now,
                Platform = Platform,
                Tables = Tables
            };

            contextItem.Add(browserInfo);
            context.SaveChanges();
        }
    }
}
