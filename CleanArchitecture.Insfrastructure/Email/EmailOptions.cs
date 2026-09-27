using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Email
{
    public sealed class EmailOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string From { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool UseSsl { get; set; }
    }
}
