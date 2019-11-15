using System.Threading.Tasks;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Interfaces
{
    public interface IWebApiService
    {
        Task<TestThing> MakeTestCall();
    }
}