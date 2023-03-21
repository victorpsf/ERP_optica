using Dapper;
using static Application.Models.Database.DatabaseModels;

namespace Application.Extensions;

public static class DatabaseExtension
{
    public static DynamicParameters toParameters(this ParameterCollection collection)
    {
        DynamicParameters parameters = new DynamicParameters();
        foreach (Parameter param in collection.parameters) 
            parameters.Add(name: param.Field, value: param.Value, direction: param.Direction);
        return parameters;
    }
}
