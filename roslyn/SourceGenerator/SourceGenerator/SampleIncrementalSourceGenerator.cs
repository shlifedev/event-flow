using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using LD.Utility;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


namespace SourceGenerator;

 

/// <summary>
/// A sample source generator that creates a custom report based on class properties. The target class should be annotated with the 'Generators.ReportAttribute' attribute.
/// When using the source code as a baseline, an incremental source generator is preferable because it reduces the performance overhead.
/// </summary>
[Generator]
public class SampleIncrementalSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        PostAttributes.CreateAttributes(context);
        
 
        var listenersCollector = context.SyntaxProvider
            .ForAttributeWithMetadataName("LD.EventFlow.Attributes.EventFlowListenerAttribute", (node, token) =>
            {
                return node is TypeDeclarationSyntax;
            }, (syntaxContext, token) =>
            {   
                return new ListenerGeneratorContext(syntaxContext);
            }).WithTrackingName("EventFlowListenerCollected");

 
        context.RegisterSourceOutput(listenersCollector.Collect(), ((productionContext, array) =>
        {
            foreach (var item in array)
            {
                if (item.IsPartial == false)
                {
                 
                }
                else
                {  
                    string registerCodeLines = string.Join("\n", item.MessageTypesWithFullName.Select(x => $"EventFlowGeneric<{x}>.Register(target);"));
                    string unregisterCodeLines = string.Join("\n", item.MessageTypesWithFullName.Select(x => $"EventFlowGeneric<{x}>.Register(target);"));
                    productionContext.AddSource($"{item.ListenerDisplayName}.EventFlow.g.cs", $@"
using {item.ListenerNameSpace};
namespace LD.Framework.EventFlow{{
public partial class EventFlow{{
        public static void Register({item.ListenerName} target)
        {{ 
            {registerCodeLines}
        }}

        public static void UnRegister({item.ListenerName} target)
        {{ 
          {unregisterCodeLines}
        }}
}}
}}
");
                }
            }
        }));


    }
    }
