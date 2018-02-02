using System.Web.Mvc;

namespace Tony.Application.Web.Areas.AuthorizeManage
{
    public class AuthorizeManageAreaRegistration:AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                AreaName + "_Default",
                AreaName + "/{controller}/{action}/{id}",
                new {area = AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] {"Tony.Application.Web.Areas." + AreaName + ".Controllers"}
            );
        }

        public override string AreaName { get { return "AuthorizeManage"; } }
    }
}