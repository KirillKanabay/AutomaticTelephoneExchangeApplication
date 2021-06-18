using ATE.Views.Base;
using Autofac;

namespace ATE.Views
{
    internal class ViewContainer
    {
        private readonly ILifetimeScope _scope;

        public ViewContainer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public BaseView Resolve<TView>() where TView : BaseView => _scope.Resolve<TView>();
    }
}
