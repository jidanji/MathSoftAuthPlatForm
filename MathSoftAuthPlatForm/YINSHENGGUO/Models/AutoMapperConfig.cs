using AutoMapper;
using DataAccess;
using DTOModel;

namespace MAZIKONG.Models
{
    public  class AutoMapperConfig
    {
        public static void Config()
        {
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


                cfg.CreateMap<Math_Work, UIMath_Work>();
                cfg.CreateMap<UIMath_Work, Math_Work>();

                cfg.CreateMap<Math_Deptinfo, UI_Math_Deptinfo>();
                cfg.CreateMap<UI_Math_Deptinfo, Math_Deptinfo>();


                cfg.CreateMap<Math_MenuInfo, UI_Math_Menuinfo>();
                cfg.CreateMap<UI_Math_Menuinfo, Math_MenuInfo>();


                cfg.CreateMap<View_User_Menu, UIRoleModel>();

                cfg.CreateMap<Math_Work_History, UIMath_Work_History>();
                cfg.CreateMap<UIMath_Work_History, Math_Work_History>();

                cfg.CreateMap<AttachedMent, UI_AttachedMent>();
                cfg.CreateMap<UI_AttachedMent, AttachedMent>();

                cfg.CreateMap<Math_Student, UIStudent>();
                cfg.CreateMap<UIStudent, Math_Student>();

                cfg.CreateMap<SysSetting, UI_SysSetting>();
                cfg.CreateMap<UI_SysSetting, SysSetting>();

                cfg.CreateMap<TimeLine, UI_TimeLine>();
                cfg.CreateMap<UI_TimeLine, TimeLine>();

                cfg.CreateMap<Math_FileUpload, Dto_Math_FileUpload>();
                cfg.CreateMap<Dto_Math_FileUpload, Math_FileUpload>();


                cfg.CreateMap<View_AnalySISS, Dto_View_AnalySISS>();
                cfg.CreateMap<Dto_View_AnalySISS, View_AnalySISS>();


                cfg.CreateMap<Dto_ZZ_Static, ZZ_Static>();
                cfg.CreateMap<ZZ_Static, Dto_ZZ_Static>();


                cfg.CreateMap<DTO_View_ZhanZhangStatic, View_ZhanZhangStatic>();
                cfg.CreateMap<View_ZhanZhangStatic, DTO_View_ZhanZhangStatic>();


                cfg.CreateMap<DTO_View_ZhanZhangStatic, ZhanZhangStaticNew>();
                cfg.CreateMap<ZhanZhangStaticNew, DTO_View_ZhanZhangStatic>();
            });
        }
    }
}
