using MonkeyChallenger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyChallenger.Services
{
    public interface IAPIComputerVision
    {
        Task<AnalyzePicture> AnalyzePicture(Stream image);
    }
}
