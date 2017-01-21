using System;
using System.Globalization;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;
using EasyTicket.Api.Controllers;
using DefaultModelBinder = System.Web.Mvc.DefaultModelBinder;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBinderProviders = System.Web.Mvc.ModelBinderProviders;
using ModelBinders = System.Web.Mvc.ModelBinders;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace EasyTicket.Api {
    public class WebApiApplication : HttpApplication {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinderProviders.BinderProviders.Add(new CustomerOrderModelBinderProvider());
            ModelBinders.Binders[typeof(TrainsRequest)] = new CustomModelBinder();
        }
    }



    public class CustomModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return base.BindModel(controllerContext, bindingContext);
        }
    }

    public class CustomerOrderModelBinderProvider : System.Web.Mvc.IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType) {
            return new CustomModelBinder();
        }
    }
}