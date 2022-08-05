using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_Bot.Models;

namespace Utility_Bot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
