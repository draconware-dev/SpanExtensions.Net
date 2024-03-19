# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.3] - 2024-3-20

### Added 

- Compatibility with **.Net 8**
- `(Readonly-)Span<T>.First()`
- `(Readonly-)Span<T>.First(Predicate<T> predicate)`
- `(Readonly-)Span<T>.FirstOrDefault()`
- `(Readonly-)Span<T>.FirstOrDefault(Predicate<T> predicate)`
- `(Readonly-)Span<T>.FirstOrDefault(T defaultValue)`
- `(Readonly-)Span<T>.FirstOrDefault(Predicate<T> predicate, T defaultValue)`
- `(Readonly-)Span<T>.Last()`
- `(Readonly-)Span<T>.Last(Predicate<T> predicate)`
- `(Readonly-)Span<T>.LastOrDefault()`
- `(Readonly-)Span<T>.LastOrDefault(Predicate<T> predicate)`
- `(Readonly-)Span<T>.LastOrDefault(T defaultValue)`
- `(Readonly-)Span<T>.LastOrDefault(Predicate<T> predicate, T defaultValue)`
- `(Readonly-)Span<T>.Single()`
- `(Readonly-)Span<T>.Single(Predicate<T> predicate)`
- `(Readonly-)Span<T>.SingleOrDefault()`
- `(Readonly-)Span<T>.SingleOrDefault(Predicate<T> predicate)`
- `(Readonly-)Span<T>.SingleOrDefault(T defaultValue)`
- `(Readonly-)Span<T>.SingleOrDefault(Predicate<T> predicate, T defaultValue)`
- `(Readonly-)Span<T>.ElementAt(int index)`
- `(Readonly-)Span<T>.ElementAt(Index index)`
- `(Readonly-)Span<T>.ElementAtOrDefault(int index)`
- `(Readonly-)Span<T>.ElementAtOrDefault(Index index)`
- `(Readonly-)Span<T>.ElementAtOrDefault(int index, T defaultValue)`
- `(Readonly-)Span<T>.ElementAtOrDefault(Index index, T defaultValue)`
- `(Readonly-)Span<T>.Min()` (https://github.com/draconware-dev/SpanExtensions.Net/pull/13)
- `(Readonly-)Span<T>.Min(Func<TSource, TResult> selector)`
- `(Readonly-)Span<T>.MinBy(Func<TSource, TKey> keySelector)`
- `(Readonly-)Span<T>.MinBy(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)`
- `(Readonly-)Span<T>.Max()` (https://github.com/draconware-dev/SpanExtensions.Net/pull/13)
- `(Readonly-)Span<T>.Max(Func<TSource, TResult> selector)`
- `(Readonly-)Span<T>.MaxBy(Func<TSource, TKey> keySelector)`
- `(Readonly-)Span<T>.MaxBy(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)`
- nuget badge to README (https://github.com/draconware-dev/SpanExtensions.Net/pull/12)
- `CountExceedingBehaviour`, which is passed to Split, defining how to properly handle its remaining elements.

### Changed 

- documentation comments to better reflect the dotnet style (https://github.com/draconware-dev/SpanExtensions.Net/pull/8)
- swapped order of `count` and `stringSplitOptions arguments` in `Split` methods.
- renamed argument `span` in `Split` methods to `source`.

### Fixed 

- empty spans being ignored if they are the last element to be returned from `Split` and are therefore not returned. (https://github.com/draconware-dev/SpanExtensions.Net/pull/10)

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
