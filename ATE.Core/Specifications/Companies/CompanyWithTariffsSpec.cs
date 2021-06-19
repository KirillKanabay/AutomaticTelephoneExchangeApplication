using ATE.Core.Entities;

namespace ATE.Core.Specifications.Companies
{
    public class CompanyWithTariffsSpec:BaseSpecification<Company>
    {
        public CompanyWithTariffsSpec()
        {
            AddInclude(c => c.Tariffs);
        }

        public CompanyWithTariffsSpec(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.Tariffs);
        }
    }
}