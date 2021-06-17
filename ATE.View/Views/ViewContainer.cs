using ATE.Views.Base;
using Autofac;

namespace ATE.Views
{
    internal class ViewContainer
    {
        private readonly IContainer _container;

        public ViewContainer(IContainer container)
        {
            _container = container;
        }

        public BaseView Resolve<TView>() where TView : BaseView => _container.Resolve<TView>();
    }
}
