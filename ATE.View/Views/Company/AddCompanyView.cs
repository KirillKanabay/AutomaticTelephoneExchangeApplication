using ATE.Views.Base;

namespace ATE.Views.Company
{
    class AddCompanyView : BaseView
    {

        public override string Title { get; protected set; } = "Automatic Telephone Exchange - Добавление компании";
        public override string Header { get; protected set; } = "Добавление компании";

        public override void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
