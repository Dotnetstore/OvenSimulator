namespace Dotnetstore.OvenSimulator.SDK;

public static class ApiEndpoints
{
    private const string Api = "/api";
    
    public static class Oven
    {
        private const string OvenBase = $"{Api}/ovens";
        
        public const string LoadRecipe = $"{OvenBase}/load-recipe";
        public const string Start = $"{OvenBase}/start";
        public const string Stop = $"{OvenBase}/stop";
        public const string Get = OvenBase;
        public const string AddError = $"{OvenBase}/add-error";
    }
    
    public static class Recipe
    {
        private const string RecipeBase = $"{Api}/recipes";
        
        public const string GetAll = RecipeBase;
        public const string GetById = $"{RecipeBase}/getById/{{id}}";
        public const string GetByName = $"{RecipeBase}/getByName/{{name}}";
        public const string Create = RecipeBase;
        public const string Update = RecipeBase;
        public const string Delete = RecipeBase;
    }
}