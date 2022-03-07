using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto
{
    public class GetContractTypeStateOutput
    {
        public GetContractTypeStateOutput(List<ContractTypeState> stats)
        {
            Stats = stats;
        }

        public GetContractTypeStateOutput()
        {
            Stats = new List<ContractTypeState>();
        }

        public List<ContractTypeState> Stats { get; set; }

    }
}