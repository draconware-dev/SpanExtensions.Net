using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace SpanExtensions.SourceGenerators
{
    static class InternalExtensions
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> source, out TKey Key, out TValue Value)
        {
            Key = source.Key;
            Value = source.Value;
        }

        public static IEnumerable<string> GetUsings(this SyntaxNode? syntaxNode)
        {
            while(syntaxNode != null && syntaxNode is not CompilationUnitSyntax)
            {
                syntaxNode = syntaxNode.Parent;
            }

            return syntaxNode is not CompilationUnitSyntax compilationSyntax
                ? []
                : compilationSyntax.Usings.Select(@using => @using.ToString());
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
            string[] attributeNames = attributeName.EndsWith("Attribute")
                ? [attributeName.Substring(0, attributeName.Length - "Attribute".Length), attributeName]
                : [attributeName, attributeName + "Attribute"];

            return syntaxNode.AttributeLists.Select(al => al.Attributes.FirstOrDefault(a => attributeNames.Contains(a.Name.ToString()))).First(a => a != null);
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

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, T skip) where T : IEquatable<T>
        {
            foreach(T element in source)
            {
                if(!element.Equals(skip))
                {
                    yield return element;
                }
            }
        }

        public static string Replace(this string source, (string find, string replace)[] findAndReplaces, (string pattern, string replacement)[] regexReplaces)
        {
            foreach((string find, string replace) in findAndReplaces)
            {
                source = source.Replace(find, replace);
            }

            foreach((string pattern, string replacement) in regexReplaces)
            {
                source = Regex.Replace(source, pattern, replacement);
            }

            return source;
        }
    }
}
