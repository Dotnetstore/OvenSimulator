using Ardalis.Result;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;

namespace Dotnetstore.OvenSimulator.Oven.Services;

internal sealed class OvenService(
    IOvenSimulator ovenSimulator) : IOvenService
{
    Result<bool> IOvenService.StartOven()
    {
        if (ovenSimulator.ActiveRecipe is null)
        {
            return Result<bool>.NotFound("No recipe is loaded. Load a recipe first.");
        }
        
        if(ovenSimulator.HeatingElementOn)
        {
            return Result<bool>.Conflict("Oven is already heating");
        }

        ovenSimulator.StartHeating();

        return Result<bool>.Success(true);
    }
    
    Result<bool> IOvenService.StopOven()
    {
        if (!ovenSimulator.HeatingElementOn)
        {
            return Result<bool>.Conflict("Oven is already stopped");
        }

        ovenSimulator.StopHeating();

        return Result<bool>.Success(true);
    }
    
    void IOvenService.AddError(AddErrorRequest req)
    {
        ovenSimulator.SimulateError(req.ErrorType);
    }
}