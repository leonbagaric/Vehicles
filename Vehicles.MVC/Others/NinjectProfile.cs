using Microsoft.AspNetCore.Identity.UI.Services;
using Ninject.Modules;
using Ninject.Web.Common;
using Vehicles.Interface;
using Vehicles.Service;

namespace Vehicles.Others
{
    public class NinjectProfile : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>().InRequestScope();
            Bind<IEmailSender>().To<NinjectEmailBypass>().InSingletonScope();
        }
    }

    //Jer ne radi register bez njega (Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UI.Services.IEmailSender')
    public class NinjectEmailBypass : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine("address: "+email + "\n subject: "+subject+"\n message: "+htmlMessage);
            return Task.CompletedTask;
        }
    }

}
