using System;
using AutoMapper;
using DataAccess;
using DTOModel;
using Newtonsoft.Json;

namespace MAZIKONG.Models
{
  public  class AutoMapperConfig
    {
        public static void Config()
        {
            //这里没有体现出mapper的强大，但是这个是入门
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Math_RoleInfo, UIRoleModel>();
                cfg.CreateMap<UIRoleModel, Math_RoleInfo>();

                cfg.CreateMap<Math_Dict, UIMath_Dict>();
                cfg.CreateMap<UIMath_Dict, Math_Dict>();

                cfg.CreateMap<Math_View_DictAndType, UIMath_View_DictAndType>();
                cfg.CreateMap<UIMath_View_DictAndType, Math_View_DictAndType>();

                cfg.CreateMap<Math_DictType, UIMath_DictType>();
                cfg.CreateMap<UIMath_DictType, Math_DictType>();

                cfg.CreateMap<Math_UserInfo, UIMathUserInfo>();
                cfg.CreateMap<UIMathUserInfo, Math_UserInfo>();


                

                cfg.CreateMap<Math_Deptinfo, UI_Math_Deptinfo>();
                cfg.CreateMap<UI_Math_Deptinfo, Math_Deptinfo>();


                cfg.CreateMap<Math_MenuInfo, UI_Math_Menuinfo>();
                cfg.CreateMap<UI_Math_Menuinfo, Math_MenuInfo>();


                cfg.CreateMap<View_User_Menu, UIRoleModel>();

               

            
 

               
 

              

 

                 


             

 
            });
        }
    }
}
