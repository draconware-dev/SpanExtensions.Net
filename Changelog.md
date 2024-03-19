# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.3] - 2024-1-25

### Changed 

- documentation comments to better reflect the dotnet style (https://github.com/draconware-dev/SpanExtensions.Net/pull/8)

## [1.2.1] - 2024-1-25

### Fixed 

- Ambiguous Extension Methods (https://github.com/draconware-dev/SpanExtensions.Net/issues/6)
- Correctness of some documentation comments     

## [1.2.0] - 2023-12-28

### Added 

- Missing documentation comments

### Fixed 

- Grammatical issues in some documentation comments

### Changed 

- moved custom Enumerators into `SpanExtensions.Enumerators` 
- declared every `GetEnumerator` method in a ref struct as `readonly`
- renamed the source ReadOnlySpan<T> in 10 out of 12 custom Enumerators from *span* to *source* 

## [1.1.0] - 2023-11-4

### Added 

- Compatibility with **.Net 6**
- Compatibility with **.Net 5**
- Compatibility with **.Net Standard 2.1**
- `(Readonly-)Span<T>.SkipWhile(Predicate<T> predicate)`
- `(Readonly-)Span<T>.SkipWhile(Func<T, int, bool> predicate)`
- `(Readonly-)Span<T>.TakeWhile(Predicate<T> predicate)`
- `(Readonly-)Span<T>.TakeWhile(Func<T, int, bool> predicate)`
- Missing documentation comments
- Changelog

### Fixed 

- Grammatical issues in some documentation comments
- Broken link to the repository on nuget ([#3](https://github.com/draconware-dev/SpanExtensions.Net/pull/3))
