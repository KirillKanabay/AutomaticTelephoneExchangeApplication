using System;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Company
{
    internal class CompanyMenuView : BaseMenuView
    {
        public override string Title { get; protected set; } = "Automatic Telephone Exchange - Управление компаниями";
        public override string Header { get; protected set; } = "Управление компаниями";

        private readonly ViewContainer _viewContainer;
        public CompanyMenuView(KeyEvent keyEvent, ViewContainer viewContainer) : base(keyEvent)
        {
            _viewContainer = viewContainer;
        }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                default:
                    break;
            }
        }

        protected override void ShowHelp()
        {

        }
    }
}