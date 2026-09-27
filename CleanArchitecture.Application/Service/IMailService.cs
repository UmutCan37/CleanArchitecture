using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Service
{
    public interface IMailService
    {
        Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default);
    }
}
