using System;
using System.Linq;
using LD.Utility;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

public readonly record struct ListenerGeneratorContext
{
    public readonly string ListenerName { get; }
    public readonly string ListenerNameSpace { get; }
    public readonly string ListenerDisplayName { get; }
    public readonly bool IsPartial { get; }
    public SyntaxKind DeclareSyntaxKind { get; }
    public readonly EquatableArray<string> MessageTypesWithFullName { get; }

    public IgnoreEquality<TypeDeclarationSyntax> OriginalClassDeclaration { get; }  
    public ListenerGeneratorContext(GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext)
    {
        var namedsymbol = generatorAttributeSyntaxContext.TargetSymbol as INamedTypeSymbol;
        var typeDeclaration = generatorAttributeSyntaxContext.TargetNode as TypeDeclarationSyntax; 
 
        
        this.IsPartial = typeDeclaration.Modifiers
            .Any(m => m.IsKind(SyntaxKind.PartialKeyword)); 
     
        this.DeclareSyntaxKind = typeDeclaration.Kind();
        
        this.ListenerDisplayName = namedsymbol.ToDisplayString();
        this.ListenerName = namedsymbol.Name;
        this.ListenerNameSpace = namedsymbol.ContainingNamespace.ToDisplayString(); 
        var messageTypeArg = namedsymbol.AllInterfaces.Where(x => x.Name == "IEventListener")
            .Where(x => x.IsGenericType);
        var typeArgsmentsDisplayName =
            messageTypeArg.Select(x => x.TypeArguments.First().ToDisplayString());
        var typeArgsmentsDisplayNameEquatableArray =
            new EquatableArray<string>(typeArgsmentsDisplayName.ToArray());
        this.MessageTypesWithFullName = typeArgsmentsDisplayNameEquatableArray;
        
        
        this.OriginalClassDeclaration = typeDeclaration;
        Console.WriteLine($"ListenerDisplayName: {ListenerDisplayName}, ListenerName: {ListenerName}, ListenerNameSpace: {ListenerNameSpace}, IsPartial: {IsPartial}, MessageTypesWithFullName: {MessageTypesWithFullName} " +
                          $" DeclareSyntaxKind: {DeclareSyntaxKind} "); 
    } 
}
public static class NodeUtility
{ 
    public static bool IsInheritListenerAttribute(ISymbol typeDeclarationSymbol)
    {

        var namedTypeSymbol = typeDeclarationSymbol as INamedTypeSymbol;
        if (namedTypeSymbol == null)
        {
            Console.WriteLine(typeDeclarationSymbol.Name +" is not a named type symbol");
            return false;
        }

        foreach(var interfaceType in namedTypeSymbol.AllInterfaces)
        {  
            
            Console.WriteLine(interfaceType.Name);
            Console.WriteLine(interfaceType.IsGenericType);
            Console.WriteLine(interfaceType.TypeArguments.Length);
        }
        return false;

    }
}