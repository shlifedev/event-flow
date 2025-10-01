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


namespace LD.EventSystem.SourceGenerator;
 
/// A sample source generator that creates a custom report based on class properties. The target class should be annotated with the 'Generators.ReportAttribute' attribute.
/// When using the source code as a baseline, an incremental source generator is preferable because it reduces the performance overhead.
/// </summary>
[Generator]
public class SampleIncrementalSourceGenerator : IIncrementalGenerator
{

    public string WrapNamespace(ListenerGeneratorContext item, string currentSource)
    {
        if (!item.ListenerNameSpace.Contains("global namespace"))
        {
            return $@"
namespace {item.ListenerNameSpace} {{
    {currentSource}
}}
";
        }

        return currentSource;
    }
    public void Initialize(IncrementalGeneratorInitializationContext context)
    { 
        var listenersCollector = context.SyntaxProvider
            .ForAttributeWithMetadataName("LD.EventSystem.Attributes.EventFlowListenerAttribute", (node, token) =>
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
                    productionContext.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor("EF0001", "EventFlowListener - Partial is missing.", $"All type declarations that use the EventFlowListener Attribute must include 'partial'. ", "Error", DiagnosticSeverity.Error, true), item.OriginalClassDeclaration.Value.GetLocation()));
                }
                else
                {  
                    string registerCodeLines = string.Join("\n", item.MessageTypesWithFullName.Select(x => $"LD.EventSystem.EventFlowGeneric<{x}>.Register(this);"));
                    string unregisterCodeLines = string.Join("\n", item.MessageTypesWithFullName.Select(x => $"LD.EventSystem.EventFlowGeneric<{x}>.UnRegister(this);"));
                    string namespaceLine = null;
                    if (item.ListenerNameSpace != "<global namespace>")
                    {
                        namespaceLine = $"using {item.ListenerNameSpace};";
                    }


                    string src = $@"
  
public partial class {item.ListenerName} {{
        public void RegisterEventListener()
        {{ 
            {registerCodeLines}
        }}

        public void UnregisterEventListener()
        {{ 
          {unregisterCodeLines}
        }}
}} 
";

                    src = WrapNamespace(item, src);
                    productionContext.AddSource($"{item.ListenerDisplayName}.EventFlow.g.cs", src);
                }
            }
        }));


    }
    }
