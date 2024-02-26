﻿using Microsoft.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

using System;
using System.CodeDom.Compiler;
using System.Globalization;

using System.IO;

#if DEBUGGENERATORS
using System.Diagnostics;
#endif

namespace SpanExtensions.SourceGenerators
{
    /// <summary>
    /// Generates a copy of the marked class/interface/struct/record and runs a set of FindAndReplace and RegexReplace operations on the copied source.
    /// </summary>
    /// <remarks>
    /// Based on <seealso href="https://github.com/LokiMidgard/SourceGenerator.Helper.CopyCode"/>.
    /// </remarks>
    [Generator(LanguageNames.CSharp)]
    sealed class CopyGenerator : IIncrementalGenerator
    {
        static readonly string generatedNamespace = typeof(CopyGenerator).Namespace;
        static readonly AssemblyName generatedAssemblyName = typeof(CopyGenerator).Assembly.GetName();
        static readonly string generatedCodeAttribute = $@"global::System.CodeDom.Compiler.GeneratedCodeAttribute(""{generatedAssemblyName.Name}"", ""{generatedAssemblyName.Version}"")";

        const string generateCopyAttributeName = "GenerateCopyAttribute";
        static readonly string generateCopyAttributeFullName = $"{generatedNamespace}.{generateCopyAttributeName}";
        static readonly string generateCopyAttributeFileName = $"{generateCopyAttributeName}.generated.cs";
        static readonly string generateCopyAttributeSource = $$"""
        // <auto-generated/>
        #nullable enable

        namespace {{generatedNamespace}}
        {
            /// <summary>
            /// Marks the class/interface/struct/record to be copied, and the FindAndReplace and RegexReplace operations to be performed on the copied source.
            /// </summary>
            [{{generatedCodeAttribute}}]
            [global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Interface | global::System.AttributeTargets.Struct, AllowMultiple = true)]
            public sealed class {{generateCopyAttributeName}} : global::System.Attribute
            {
                /// <summary>
                /// The FindAndReplace operations to be performed on the copied source.
                /// </summary>
                public string[] FindAndReplaces { get; set; } = global::System.Array.Empty<string>();

                /// <summary>
                /// The RegexReplace operations to be performed on the copied source.
                /// </summary>
                public string[] RegexReplaces { get; set; } = global::System.Array.Empty<string>();

                /// <summary>
                /// The tag to add to the generated file name.
                /// </summary>
                /// <remarks>
                /// Used when generating several copies of the same partial type.
                /// </remarks>
                public string GeneratedFileTag { get; set; } = "";
            }
        }
        """;

        const string indentation = "    ";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(static context => context.AddSource(generateCopyAttributeFileName, SourceText.From(generateCopyAttributeSource, Encoding.UTF8)));

            IncrementalValuesProvider<Capture> provider = context.SyntaxProvider
                .ForAttributeWithMetadataName(generateCopyAttributeFullName, SyntaxProviderPredicate, SyntaxProviderTransform)
                .Where(static capture => capture != null)
                .WithComparer(Capture.EqualityComparer)!;

            IncrementalValuesProvider<Capture> failed = provider.Where(static capture => capture!.DiagnosticMessage != null);
            context.RegisterImplementationSourceOutput(failed, static (context, capture) => ReportDiagnostic(context, capture.DiagnosticMessage!, capture.DiagnosticMessageLocation!, capture.DiagnosticMessageArgs!));

            IncrementalValuesProvider<Capture> successful = provider.Where(static capture => capture!.DiagnosticMessage == null);
            context.RegisterSourceOutput(successful, static (context, capture) => GenerateSource(context, capture));
        }

        static bool SyntaxProviderPredicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
        {
            return syntaxNode is TypeDeclarationSyntax;
        }

        static Capture? SyntaxProviderTransform(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
#if DEBUGGENERATORS
            if (!Debugger.IsAttached) Debugger.Launch();
#endif

            var syntaxNode = (TypeDeclarationSyntax)context.TargetNode;
            AttributeSyntax attributeSyntax = syntaxNode.GetAttributeSyntax(generateCopyAttributeName);

            string[] usings = syntaxNode.GetUsings().Skip("using SpanExtensions.SourceGenerators;").ToArray();

            cancellationToken.ThrowIfCancellationRequested();

            // We need the symbol to get the namespace, using parent syntax could not work with FileScopedNamespaceDeclaration
            if(context.TargetSymbol is not INamedTypeSymbol targetAttributeTypeSymbol)
            {
                return new("SyntaxNode {0} can't get INamedTypeSymbol.", syntaxNode.GetLocation(), syntaxNode);
            }

            (string find, string replace)[] findAndReplaces = [];
            (string pattern, string replacement)[] regexReplaces = [];
            string generatedFileTag = "";
            AttributeData attribute = context.Attributes.First(a => a.AttributeClass!.Name == generateCopyAttributeName);
            foreach((string parameter, TypedConstant value) in attribute.NamedArguments)
            {
                switch(parameter)
                {
                    case "FindAndReplaces":
                        string[] strings = value.Values.Select(v => (string)v.Value!).ToArray();

                        if(strings.Length % 2 != 0)
                        {
                            return new("Attribute {0} requres the parameter {1} have even number of values, but {2} were defined.", attributeSyntax.GetLocation(), generateCopyAttributeName, parameter, strings.Length);
                        }

                        findAndReplaces = new (string find, string replace)[strings.Length / 2];
                        for(int i = 0, j = 0; j < strings.Length; i++, j += 2)
                        {
                            findAndReplaces[i] = (strings[j], strings[j + 1]);
                        }
                        break;
                    case "RegexReplaces":
                        strings = value.Values.Select(v => (string)v.Value!).ToArray();

                        if(strings.Length % 2 != 0)
                        {
                            return new("Attribute {0} requres the parameter {1} have even number of values, but {2} were defined.", attributeSyntax.GetLocation(), generateCopyAttributeName, parameter, strings.Length);
                        }

                        regexReplaces = new (string find, string replace)[strings.Length / 2];
                        for(int i = 0, j = 0; j < strings.Length; i++, j += 2)
                        {
                            regexReplaces[i] = (strings[j], strings[j + 1]);
                        }
                        break;
                    case "GeneratedFileTag":
                        generatedFileTag = value.Value.ToString();
                        break;
                    default:
                        return new("Unrecognized parameter {0} for attribute {1}.", attributeSyntax.GetLocation(), parameter, generateCopyAttributeName);
                }
            }
            if(findAndReplaces.Length == 0 && regexReplaces.Length == 0)
            {
                return new("Attribute {0} requres either FindAndReplaces or RegexReplaces be specified.", attributeSyntax.GetLocation(), generateCopyAttributeName);
            }

            cancellationToken.ThrowIfCancellationRequested();

            const bool replaceNamespace = false;
            string @namespace = targetAttributeTypeSymbol.GetNamespace();
            string namespaceReplaced = replaceNamespace ? @namespace.Replace(findAndReplaces, regexReplaces) : @namespace;

            TypeDeclaration[] nestedDeclarations = TypeDeclaration.GetNestedDeclarations(syntaxNode);
            TypeDeclaration[] nestedDeclarationsReplaced = nestedDeclarations.Select(d =>
            {
                string newName = d.Name.Replace(findAndReplaces, regexReplaces);
                return newName == d.Name ? d : d.WithName(newName);
            }).ToArray();

            if(@namespace == namespaceReplaced)
            {
                for(int i = 1; i < nestedDeclarations.Length; i++)
                {
                    if(nestedDeclarations[i] != nestedDeclarationsReplaced[i])
                    {
                        break;
                    }

                    if(!nestedDeclarations[i].Modifiers.Contains("partial"))
                    {
                        SyntaxNode parent = syntaxNode;
                        while(i-- != 0)
                        {
                            parent = parent.Parent!;
                        }

                        Location location = parent is TypeDeclarationSyntax declarationWithoutPartial
                            ? declarationWithoutPartial.Identifier.GetLocation()
                            : syntaxNode.Identifier.GetLocation();

                        return new("Type must be declared as \"partial\"", location);
                    }
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            TypeDeclarationSyntax syntaxToCopy = syntaxNode.WithAttributeLists(
                new SyntaxList<AttributeListSyntax>(
                    syntaxNode.AttributeLists.Select(al => al.RemoveIfContains(attributeSyntax)).Where(al => al.Attributes.Count != 0)
                )
            );
            syntaxToCopy = syntaxToCopy.WithLeadingTrivia(syntaxNode.GetLeadingTrivia());
            string sourceCode = syntaxToCopy.NormalizeWhitespace(indentation: indentation, eol: "\n").ToFullString();

            cancellationToken.ThrowIfCancellationRequested();

            return new(
                usings: usings,
                @namespace: namespaceReplaced,
                nestedTypeDeclarations: nestedDeclarationsReplaced,
                sourceCode: sourceCode,
                findAndReplaces: findAndReplaces,
                regexReplaces: regexReplaces,
                generatedFileTag: generatedFileTag
            );
        }

        static void ReportDiagnostic(SourceProductionContext context, string diagnosticMessage, Location location, params object?[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor("GEN1", "SourceGenerators", diagnosticMessage, "Generation", DiagnosticSeverity.Error, true),
                location,
                messageArgs
            ));
        }

        static void GenerateSource(SourceProductionContext context, Capture capture)
        {
            if(capture.DiagnosticMessage != null)
            {
                ReportDiagnostic(context, capture.DiagnosticMessage, capture.DiagnosticMessageLocation!, capture.DiagnosticMessageArgs!);
                return;
            }

            StringBuilder sourceBuilder = new();
            using StringWriter _sourceWriter = new(sourceBuilder, CultureInfo.InvariantCulture);
            using IndentedTextWriter sourceWriter = new(_sourceWriter, indentation);

            sourceWriter.WriteLine("// <auto-generated/>");
            sourceWriter.WriteLine("#nullable enable");
            sourceWriter.WriteLine();
            foreach(string @using in capture.Usings)
            {
                sourceWriter.WriteLine(@using);
            }
            sourceWriter.WriteLine();
            sourceWriter.WriteLine($"namespace {capture.Namespace}");
            sourceWriter.WriteLine('{');
            sourceWriter.Indent++; // namespace
            sourceWriter.WriteLine();

            foreach(TypeDeclaration? type in capture.NestedTypeDeclarations.Skip(1).Reverse())
            {
                sourceWriter.WriteLine(type.ToString());
                sourceWriter.WriteLine('{');
                sourceWriter.Indent++; // parent declarations
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            string sourceCode = capture.SourceCode.Replace(capture.FindAndReplaces, capture.RegexReplaces);
            foreach(string line in sourceCode.Split('\n'))
            {
                sourceWriter.WriteLine(line.TrimEnd('\r'));
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            for(int i = 0; i < capture.NestedTypeDeclarations.Length - 1; i++)
            {
                sourceWriter.Indent--; // parent declarations
                sourceWriter.WriteLine("}");
            }

            sourceWriter.Indent--; // namespace
            sourceWriter.WriteLine("}");

#if DEBUGGENERATORS
            Debug.Assert(sourceWriter.Indent == 0);
#endif

            context.CancellationToken.ThrowIfCancellationRequested();

            sourceCode = sourceBuilder.ToString();

            string fileTag = string.IsNullOrEmpty(capture.GeneratedFileTag) ? "0" : capture.GeneratedFileTag;
            context.AddSource($"{capture.UniqueName}.{generateCopyAttributeName}.{fileTag}.generated.cs", sourceCode);
        }

        sealed class Capture(string[] usings, string @namespace, TypeDeclaration[] nestedTypeDeclarations, string sourceCode, (string find, string replace)[] findAndReplaces, (string pattern, string replacement)[] regexReplaces, string generatedFileTag) : IEquatable<Capture>
        {
            public string[] Usings { get; } = usings;
            public string Namespace { get; } = @namespace;
            public TypeDeclaration[] NestedTypeDeclarations { get; } = nestedTypeDeclarations;
            public string SourceCode { get; } = sourceCode;
            public (string find, string replace)[] FindAndReplaces { get; } = findAndReplaces;
            public (string pattern, string replacement)[] RegexReplaces { get; } = regexReplaces;
            public string GeneratedFileTag { get; } = generatedFileTag;

            public string? DiagnosticMessage { get; }
            public object?[]? DiagnosticMessageArgs { get; }
            public Location? DiagnosticMessageLocation { get; }

            Capture() : this([], "", [], "", [], [], "") { }

            public Capture(string diagnosticMessage, params object?[]? diagnosticMessageArgs) : this()
            {
                DiagnosticMessage = diagnosticMessage;
                DiagnosticMessageArgs = diagnosticMessageArgs;
                DiagnosticMessageLocation = Location.None;
            }

            public Capture(string diagnosticMessage, Location diagnosticMessageLocation, params object?[]? diagnosticMessageArgs) : this()
            {
                DiagnosticMessage = diagnosticMessage;
                DiagnosticMessageArgs = diagnosticMessageArgs;
                DiagnosticMessageLocation = diagnosticMessageLocation;
            }

            public string UniqueName
            {
                get
                {
                    return string.Join(".", NestedTypeDeclarations.Select(d => d.Name).And(Namespace).Reverse());
                }
            }

            public override bool Equals(object? obj) =>
                obj is Capture other && Equals(other);

            public bool Equals(Capture other) =>
                Usings.AsSpan().SequenceEqual(other.Usings) &&
                Namespace == other.Namespace &&
                NestedTypeDeclarations.AsSpan().SequenceEqual(other.NestedTypeDeclarations) &&
                SourceCode == other.SourceCode &&
                FindAndReplaces.AsSpan().SequenceEqual(other.FindAndReplaces) &&
                RegexReplaces.AsSpan().SequenceEqual(other.RegexReplaces) &&
                GeneratedFileTag == other.GeneratedFileTag &&
                DiagnosticMessage == other.DiagnosticMessage;

            public override int GetHashCode()
            {
                const int multiplier = -1521134295;
                int hashCode = -573548719;
                hashCode = (hashCode * multiplier) + EqualityComparer<string[]>.Default.GetHashCode(Usings);
                hashCode = (hashCode * multiplier) + EqualityComparer<string>.Default.GetHashCode(Namespace);
                hashCode = (hashCode * multiplier) + EqualityComparer<TypeDeclaration[]>.Default.GetHashCode(NestedTypeDeclarations);
                hashCode = (hashCode * multiplier) + EqualityComparer<string>.Default.GetHashCode(SourceCode);
                hashCode = (hashCode * multiplier) + EqualityComparer<(string, string)[]>.Default.GetHashCode(FindAndReplaces);
                hashCode = (hashCode * multiplier) + EqualityComparer<(string, string)[]>.Default.GetHashCode(RegexReplaces);
                hashCode = (hashCode * multiplier) + EqualityComparer<string>.Default.GetHashCode(GeneratedFileTag);
                hashCode = (hashCode * multiplier) + EqualityComparer<string>.Default.GetHashCode(DiagnosticMessage ?? "");
                return hashCode;
            }

            public sealed class Comparer : IEqualityComparer<Capture?>
            {
                bool IEqualityComparer<Capture?>.Equals(Capture? x, Capture? y) =>
                    x != null && y != null && x.Equals(y);

                int IEqualityComparer<Capture?>.GetHashCode(Capture? generator) =>
                    generator?.GetHashCode() ?? 0;
            }

            public static readonly Comparer EqualityComparer = new();
        }

        internal sealed class TypeDeclaration(string modifiers, string keyword, string name, string typeParameters, string constraints) : IEquatable<TypeDeclaration>
        {
            public string Modifiers { get; } = modifiers;
            public string Keyword { get; } = keyword;
            public string Name { get; } = name;
            public string TypeParameters { get; } = typeParameters;
            public string Constraints { get; } = constraints;

            public TypeDeclaration WithName(string name)
            {
                return new(Modifiers, Keyword, name, TypeParameters, Constraints);
            }

            public static TypeDeclaration[] GetNestedDeclarations(TypeDeclarationSyntax typeSyntax)
            {
                var declarations = new List<TypeDeclaration>();

                TypeDeclarationSyntax? parentSyntax = typeSyntax;
                do
                {
                    declarations.Add(new(
                        modifiers: parentSyntax.Modifiers.ToString(),
                        keyword: parentSyntax.Keyword.ValueText,
                        name: parentSyntax.Identifier.ToString(),
                        typeParameters: parentSyntax.TypeParameterList?.ToString() ?? "",
                        constraints: parentSyntax.ConstraintClauses.ToString()
                    ));

                    parentSyntax = parentSyntax.Parent as TypeDeclarationSyntax;
                }
                while(parentSyntax != null && IsAllowedKind(parentSyntax.Kind()));

                return [..declarations];
            }

            static bool IsAllowedKind(SyntaxKind kind) =>
                kind is SyntaxKind.ClassDeclaration or
                SyntaxKind.InterfaceDeclaration or
                SyntaxKind.StructDeclaration or
                SyntaxKind.RecordDeclaration;

            public override bool Equals(object? obj) =>
                obj is TypeDeclaration other && Equals(other);

            public bool Equals(TypeDeclaration other) =>
                Modifiers == other.Modifiers &&
                Keyword == other.Keyword &&
                Name == other.Name &&
                TypeParameters == other.TypeParameters &&
                Constraints == other.Constraints;

            public override int GetHashCode()
            {
                static int StringHash(string s) => EqualityComparer<string>.Default.GetHashCode(s);

                const int multiplier = -1521134295;
                int hashCode = -754136522;
                hashCode = (hashCode * multiplier) + StringHash(Modifiers);
                hashCode = (hashCode * multiplier) + StringHash(Keyword);
                hashCode = (hashCode * multiplier) + StringHash(Name);
                hashCode = (hashCode * multiplier) + StringHash(TypeParameters);
                hashCode = (hashCode * multiplier) + StringHash(Constraints);
                return hashCode;
            }

            public override string ToString()
            {
                string type = $"{Modifiers} {Keyword} {Name}{TypeParameters}";

                return Constraints.Length != 0 ? $"{type} {string.Join(" ", Constraints)}" : type;
            }
        }
    }
}
