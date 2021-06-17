using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Company
{
    internal class CompanyMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;
        public CompanyMenuView(KeyEvent keyEvent, ViewContainer viewContainer) : base(keyEvent)
        {
            _viewContainer = viewContainer;
        }

        public override void Show()
        {
            
        }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                default:
                    break;
            }
        }
    }
}