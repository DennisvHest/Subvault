using Ninject;
using Subvault_Domain.Abstract;
using Subvault_Domain.Concrete;
using Subvault_UI.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Subvault_UI.Infrastructure {

    public class NinjectDependencyResolver : IDependencyResolver {

        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {
            EFDbContext context = new EFDbContext();

            kernel.Bind<EFDbContext>().ToConstant(context);

            kernel.Bind<IItemRepository>().To<ItemRepository>();
            kernel.Bind<IItemAPIRepository>().To<ItemAPIRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ISubtitlesRepository>().To<SubtitlesRepository>();

            kernel.Bind<ItemManager>().To<ItemManager>();
            kernel.Bind<UserManager>().To<UserManager>();
            kernel.Bind<SubtitlesManager>().To<SubtitlesManager>();
            kernel.Bind<FileManager>().To<FileManager>();
        }
    }
}