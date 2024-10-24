namespace Dotnetstore.OvenSimulator.SDK.Oven.Responses;

public record struct GetOvenStatusResponse(
    double CurrentTemperature, 
    bool HeatingElementOn,
    string ActiveRecipe,
    string CurrentError);