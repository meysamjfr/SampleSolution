using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Repositories.Infrastructure
{
    public interface ISmsSender
    {
        Task<bool> SimpleSend(Sms sms);
    }
}
