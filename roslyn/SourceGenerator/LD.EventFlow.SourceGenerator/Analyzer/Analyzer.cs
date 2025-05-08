// using System;
// using System.Collections.Immutable;
// using System.Linq;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
// using Microsoft.CodeAnalysis.Diagnostics;
//
// [DiagnosticAnalyzer(LanguageNames.CSharp)]
// public class MyAnalyzer : DiagnosticAnalyzer
// {
//     private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//         "EF3001", 
//         "OnEvent 오류", 
//         "Method '{0}' contains 'Console.WriteLine'.", 
//         "Usage", 
//         DiagnosticSeverity.Warning, 
//         isEnabledByDefault: true);
//     public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
//
//     public override void Initialize(AnalysisContext context)
//     {  
//         context.RegisterSyntaxNodeAction(AnalyizeMessage, SyntaxKind.StructDeclaration, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
//     }
//
//     private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
//     {
//        
//     }
//     
//     private void AnalyizeMessage(SyntaxNodeAnalysisContext context)
//     {
//             var namedTypeSymbol = context.ContainingSymbol as INamedTypeSymbol; 
//             
//             var isInheritEventMessageInterface = IsInheritEventMessageInterface(namedTypeSymbol);
//             // struct 타입인지?
//             var isStruct = ((context.Node as TypeDeclarationSyntax)!).Modifiers.Any(SyntaxKind.StructKeyword); 
//             if (!isStruct)
//             {
//                 
//             }
//
//     }
//     
//     private bool IsInheritEventMessageInterface(INamedTypeSymbol typeSymbol)
//     { 
//         foreach (var @interface in typeSymbol.AllInterfaces)
//         { 
//             if (@interface.Name == "IEventMessage")
//             {
//                 return true;
//             }
//             return IsInheritEventMessageInterface(@interface);
//         }
//
//         return false;
//     }
// }