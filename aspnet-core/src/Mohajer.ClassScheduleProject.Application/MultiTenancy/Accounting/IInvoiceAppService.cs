using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Accounting.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
