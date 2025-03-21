using System;
using LD.Utility;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

public readonly record struct ListenerGeneratorContext
{
    public readonly string ListenerName { get; }
    public readonly bool IsPartial { get; }
    public readonly EquatableArray<string> MessageTypesWithFullName { get; }
    
    public ListenerGeneratorContext(string listenerName, bool isPartial, EquatableArray<string> messageTypesWithFullName)
    {
        Console.WriteLine("리스너 이름 : " + listenerName + " isPartial : " + isPartial + " 메시지 타입 : " + messageTypesWithFullName);
        ListenerName = listenerName;
        IsPartial = isPartial;
        MessageTypesWithFullName = messageTypesWithFullName;
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