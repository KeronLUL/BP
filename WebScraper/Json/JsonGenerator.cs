using System.Reflection;
using System.Text;
using WebScraper.Arguments;

namespace WebScraper.Json;

public static class JsonGenerator
{
    private static string GetParameterType(Type parameter)
    {
        switch (parameter.ToString())
        {
            case "System.String":
                return "string";
            case "System.Int32":
                return "integer";
        }

        return "";
    }
    
    public static void GenerateJson(String path)
    {
        Args.PrintVerbose("Generating JSON schema...");
        using StreamWriter sw = File.CreateText(path);
        StringBuilder json = new StringBuilder($@"
{{
   ""$id"": ""/webscraper"",
   ""type"": ""object"",
   ""properties"": {{
     ""url"": {{
       ""type"": ""string""
     }},
     ""driver"": {{
       ""type"": ""string"",
       ""pattern"": ""^(Chrome|Firefox|Safari)$""
     }},
     ""loop"": {{
       ""type"": ""boolean""
     }},
     ""waitTime"": {{
       ""type"": ""integer"",
       ""minimum"" : 0
     }},
     ""commands"": {{
       ""type"": ""array"",
       ""items"": {{
         ""type"": ""object"",
         ""properties"": {{");
            
        var assembly = Assembly.GetExecutingAssembly().GetTypes();
        assembly = assembly.Where(name => name.FullName!.Contains("SeleniumCommands")).ToArray();
        foreach (var command in assembly) if ( command.IsPublic )
        {
            var name = command.FullName!.Split(".")[1];
            var parameters = command.GetMethods()[0].GetParameters();

            json.Append($@"
           ""{name}"": {{
             ""type"": ""object"",
             ""properties"": {{
               ""args"": {{
                 ""type"": ""object"",
                 ""properties"": {{");
                
            foreach (var param in parameters)
            {
                json.Append($@"
                   ""{param.Name}"": {{
                     ""type"": ""{GetParameterType(param.ParameterType)}""");
                if (param.Name == "time")
                {
                    json.Append($@",
                     ""minimum"" : 0");
                }
                json.Append($@"
                   }}");
                if (param != parameters.Last())
                { 
                    json.Append(',');
                }
            }
            json.Append($@"
                 }},
                 ""additionalProperties"": false,
                 ""minProperties"": {parameters.Length}
               }}
             }},
             ""additionalProperties"": false
           }}");
            if ( command != assembly.Last() )
            {
                json.Append(',');
                
            }
        }
        json.Append($@"
         }},
         ""additionalProperties"": false,
         ""maxProperties"": 1
       }}
     }}
   }},
   ""additionalProperties"": false,
   ""required"": [""url"", ""loop"", ""waitTime""]
}}");
            
        sw.Write(json.ToString());
        Args.PrintVerbose($@"Done generating JSON schema '{path}'.");
    }
}