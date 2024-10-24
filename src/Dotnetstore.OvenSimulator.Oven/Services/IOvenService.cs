using Ardalis.Result;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;

namespace Dotnetstore.OvenSimulator.Oven.Services;

public interface IOvenService
{
    Result<bool> StartOven();
    
    Result<bool> StopOven();
    
    void AddError(AddErrorRequest req);
}