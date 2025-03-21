using Microsoft.CodeAnalysis;

namespace SourceGenerator;
 
public static class PostAttributes
{ 
    public static void CreateAttributes(IncrementalGeneratorInitializationContext initContext)
    {
        initContext.RegisterPostInitializationOutput(context =>
        {
            context.AddSource("EventFlowAttributes.g.cs", @$"
namespace LD.EventFlow.Attributes
{{
    public class EventFlowListenerAttribute : System.Attribute
    {{
        
    }}
}}
");
        });
    }

}