using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

namespace Dotnetstore.OvenSimulator.Oven.Services;

internal interface IOvenSimulator
{
    public RecipeResponse? ActiveRecipe { get; internal set; }
    
    public bool HeatingElementOn { get; internal set; }
    
    public double AmbientTemperature { get; set; }

    public double TargetTemperature { get; set; }
    
    public double CurrentTemperature { get; internal set; }
    
    public OvenErrorType CurrentError { get; internal set; }
    
    void StartHeating();
    
    void StopHeating();
    
    void Update(double deltaTime);
    
    void RungeKuttaStep(double deltaTime);
    
    void SimulateError(OvenErrorType error);
}