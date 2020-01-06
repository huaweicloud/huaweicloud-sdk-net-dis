using Com.Bigdata.Dis.Sdk.DISCommon.Interface;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System.Threading.Tasks;

namespace DISClient_4._5.Client
{
    public interface IDISAsync : IDIS
    {
        Task<int> PutFilesAsync(PutRecordsRequest putFilesRequest);
    }
}
