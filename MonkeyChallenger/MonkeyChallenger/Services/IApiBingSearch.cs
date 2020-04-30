using MonkeyChallenger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyChallenger.Services
{
    public interface IApiBingSearch
    {
        Task<List<Picture>> GetBingPicture();
    }
}
