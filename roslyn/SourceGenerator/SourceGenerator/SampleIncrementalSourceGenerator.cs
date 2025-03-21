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
                
                var namedsymbol = syntaxContext.TargetSymbol as INamedTypeSymbol;
                var typeDeclaration = syntaxContext.TargetNode as TypeDeclarationSyntax; 
                bool isPartial = typeDeclaration.Modifiers
                    .Any(m => m.IsKind(SyntaxKind.PartialKeyword));
                
                if (namedsymbol != null)
                {
                    var messageTypeArg = namedsymbol.AllInterfaces.Where(x => x.Name == "IEventListener")
                        .Where(x => x.IsGenericType);


                    var typeArgsmentsDisplayName =
                        messageTypeArg.Select(x => x.TypeArguments.First().ToDisplayString());
                    var typeArgsmentsDisplayNameEquatableArray =
                        new EquatableArray<string>(typeArgsmentsDisplayName.ToArray()); 
                    return new ListenerGeneratorContext(namedsymbol.ToDisplayString(), isPartial,
                        typeArgsmentsDisplayNameEquatableArray);
                }

                return default;
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
                    productionContext.AddSource($"{item.ListenerName}_Register.g.cs", "//"+item.MessageTypesWithFullName);
                }
            }
        }));


    }
    }
