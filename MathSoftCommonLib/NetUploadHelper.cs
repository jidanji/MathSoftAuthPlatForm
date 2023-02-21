using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace MathSoftCommonLib
{
    #region 上传帮助类
    /// <summary>
    /// 上传帮助类
    /// </summary>
    public class NetUploadHelper
    {
        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="Server"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpLoadPic(FileUpload FileUpload1, HttpServerUtility Server, out string message)
        {
            if (FileUpload1.HasFile)
            {
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".bmp" || Extension == ".jpeg" || Extension == ".png")
                {
                }
                else
                {
                    message = "文件的后缀必须为 .jpg .bmp .jpeg .png";

                    return false;
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(10000, 99999).ToString() + Extension;
                string virtualPath = "~/Admin/UpLoad/" + fileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath(virtualPath));

                message = string.Format("Admin/UpLoad/{0}", fileName);
                return true;
            }
            //没有
            else
            {
                message = "没有文件";
                return false;
            }
        }
        #endregion

    }
    #endregion
}
