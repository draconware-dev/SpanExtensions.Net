using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;

namespace SpanExtensions.SourceGenerators
{
    static class InternalExtensions
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> source, out TKey Key, out TValue Value)
        {
            Key = source.Key;
            Value = source.Value;
        }

        public static string[] GetUsings(this SyntaxNode? syntaxNode)
        {
            while(syntaxNode != null && syntaxNode is not CompilationUnitSyntax)
            {
                syntaxNode = syntaxNode.Parent;
            }

            return syntaxNode is not CompilationUnitSyntax compilationSyntax
                ? (string[])([])
                : compilationSyntax.Usings.Select(@using => @using.ToString()).ToArray();
        }

        public static string GetNamespace(this INamedTypeSymbol namedTypeSymbol)
        {
            INamespaceSymbol? currentNameSpace = namedTypeSymbol.ContainingNamespace;

            if(currentNameSpace?.IsGlobalNamespace != false)
            {
                return "";
            }

            List<string> namespaceParts = [];
            do
            {
                if(!currentNameSpace.IsGlobalNamespace)
                {
                    namespaceParts.Add(currentNameSpace.Name);
                }
            }
            while((currentNameSpace = currentNameSpace!.ContainingNamespace) is not null);

            namespaceParts.Reverse();
            return string.Join(".", namespaceParts);
        }

        public static AttributeSyntax GetAttributeSyntax(this TypeDeclarationSyntax syntaxNode, string attributeName)
        {
            string attributeFullName = "";
            if(attributeName.EndsWith("Attribute"))
            {
                attributeFullName = attributeName;
                attributeName = attributeName.Substring(0, attributeName.Length - "Attribute".Length);
            }
            else
            {
                attributeFullName = attributeName + "Attribute";
            }

            return syntaxNode.AttributeLists.Select(al => al.Attributes.FirstOrDefault(a => a.Name.ToString() == attributeName || a.Name.ToString() == attributeFullName)).First(a => a != null);
        }

        public static AttributeListSyntax RemoveIfContains(this AttributeListSyntax list, AttributeSyntax attribute)
        {
            if (!list.Contains(attribute))
            {
                return list;
            }

            return list.RemoveNode(attribute, SyntaxRemoveOptions.KeepNoTrivia)!;
        }

        public static IEnumerable<T> And<T>(this IEnumerable<T> source, T after)
        {
            foreach(T element in source)
            {
                yield return element;
            }

            yield return after;
        }
    }
}
