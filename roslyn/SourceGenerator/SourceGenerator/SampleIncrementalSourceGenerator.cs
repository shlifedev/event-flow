using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


namespace SourceGenerator;


/*
 * 수집항목
 * IEventListener<TMessage> 를 상속받는 모든 리스너 클래스
 */

/// <summary>
/// A sample source generator that creates a custom report based on class properties. The target class should be annotated with the 'Generators.ReportAttribute' attribute.
/// When using the source code as a baseline, an incremental source generator is preferable because it reduces the performance overhead.
/// </summary>
[Generator]
public class SampleIncrementalSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
                                                
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource("test.g.cs", SourceText.From($@" 

public class SampleAttribute : System.Attribute{{

}}

", Encoding.UTF8));
        });


        IncrementalValuesProvider<string> findClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) =>
                {
                    return s is ClassDeclarationSyntax;
                },
                transform: static (ctx, _) =>
                {
                    var classDeclaration = (ClassDeclarationSyntax)ctx.Node;
                    var semanticModel = ctx.SemanticModel;
                    var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
                    var eventListener = classSymbol.AllInterfaces.All(x=>x.Name == "IEventListener" && x.IsGenericType);
                    

                    Console.WriteLine($"{classSymbol.Name}  "+classSymbol.AllInterfaces
                        .Select(static x => x.Name)
                        .Aggregate(new StringBuilder(), (sb, x) => sb.Append(x + ", ")));
                    return "";
                    
                    // foreach (var interfaceSymbol in classSymbol.al)
                    // {
                    //     if (interfaceSymbol.Name == "IEventListener" && 
                    //         interfaceSymbol.IsGenericType)
                    //     {
                    //         return classDeclaration.Identifier.Text;
                    //     }
                    // }
                    // if (classSymbol == null)
                    //     return string.Empty;
                    //
                })                                  
            .Where(static x => !string.IsNullOrWhiteSpace(x));

        context.RegisterSourceOutput(findClasses,
            static (spc, source) =>
            {
                Console.WriteLine(source);
            });
    }
    }
