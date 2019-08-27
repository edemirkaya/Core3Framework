using CommonCore.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CommonCore.Web.Infrastructure
{
    public class AuthorizationFilter : IActionFilter
    {

        private IAuthorizationManager authorizationManager;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            authorizationManager = filterContext.HttpContext.RequestServices.GetService<IAuthorizationManager>();
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                var unauthorizedAccessAttributeMain = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(UnauthorizedAccessAttribute), false).FirstOrDefault();
                if (unauthorizedAccessAttributeMain != null)
                {
                    UnauthorizedAccessAttribute unauthorizedAccessAttribute = (UnauthorizedAccessAttribute)unauthorizedAccessAttributeMain;
                    if (unauthorizedAccessAttribute.MustAuthenticated &&
                        !filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        filterContext.Result = new StatusCodeResult(HttpStatusCode.Forbidden.GetHashCode());
                        return;
                    }

                    return;
                }
            }

            ////login olmayan kullanici sadece action veya controller üzerinde UnauthorizedAccess var ise giriş yapabilir.
            if (filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                List<AuthorizeFunctionAttribute> authorizeFunctionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AuthorizeFunctionAttribute), false).ToList().Cast<AuthorizeFunctionAttribute>().ToList();
                if (authorizeFunctionAttributes.Any())
                {

                    var authorizeFlag = false;
                    authorizeFunctionAttributes.ForEach(
                        p => authorizeFlag |= authorizationManager.IsAuthorisedForFunctionAsync(p.FunctionIds).Result);

                    //Kullanici fonksiyona yetkili degil - Reject
                    if (!authorizeFlag)
                    {
                        filterContext.Result = new StatusCodeResult(HttpStatusCode.Forbidden.GetHashCode());
                        return;
                    }
                    //yetkili
                    return;
                }

                //action metodu üzerinde AuthorizePageAttribute attribute var, hakkı check et 
                List<AuthorizePageAttribute> authorizePageAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AuthorizePageAttribute), false).ToList().Cast<AuthorizePageAttribute>().ToList();

                if (authorizePageAttributes.Any())
                {
                    var authorizeFlag = false;
                    authorizePageAttributes.ForEach(
                        p => authorizeFlag |= authorizationManager.IsAuthorisedForPage(p.PageIds).Result);


                    //Action üzerinde AuthorizePageAttribute var, üye ona yetkilimi diye bakılacak
                    if (!authorizeFlag)
                    {
                        filterContext.Result = new StatusCodeResult(HttpStatusCode.Forbidden.GetHashCode());
                        return;
                    }
                    //yetkili
                    return;
                }

                //controller üzerinde AuthorizePageAttribute attribute var, hakkı check et                
                List<AuthorizePageAttribute> sayfaAttributeOnController = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(AuthorizePageAttribute), false).ToList().Cast<AuthorizePageAttribute>().ToList();

                if (sayfaAttributeOnController.Any())
                {
                    var authorizeFlag = false;
                    sayfaAttributeOnController.ForEach(
                        p => authorizeFlag |= authorizationManager.IsAuthorisedForPage(p.PageIds).Result);


                    //Controller üzerinde AuthorizePageAttribute var, kullanici ona yetkilimi diye bakılacak
                    if (!authorizeFlag)
                    {
                        filterContext.Result = new StatusCodeResult(HttpStatusCode.Forbidden.GetHashCode());
                        return;
                    }
                    //yetkili
                    return;
                }
            }

            ////login olmus ama hic bir yetkisi yok, reject
            filterContext.Result = new StatusCodeResult(HttpStatusCode.Forbidden.GetHashCode());

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }   
}
