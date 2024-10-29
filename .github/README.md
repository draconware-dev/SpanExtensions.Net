# SpanExtensions

![NuGet Version](https://img.shields.io/nuget/v/SpanExtensions.Net)
![NuGet Downloads](https://img.shields.io/nuget/dt/SpanExtensions.Net)
![GitHub License](https://img.shields.io/github/license/draconware-dev/SpanExtensions.Net)

## About
**`ReadonlySpan<T>`** and **`Span<T>`** are great Types in _C#_, but unfortunately working with them can sometimes be sort of a hassle and some use cases seem straight up impossible, even though they are not.  

**SpanExtensions.Net** aims to help developers use `ReadonlySpan<T>` and `Span<T>` more **productively**, **efficiently** and **safely** and write overall more **performant** Programs.  
 
Never again switch back to using `string` instead of `ReadonlySpan<T>`, just because the method you seek is not supported.  
 
**SpanExtensions.Net** provides alternatives for many missing Extension Methods for **`ReadonlySpan<T>`** and **`Span<T>`**, ranging from `string.Split()` over `Enumerable.Skip()` and `Enumerable.Take()` to an improved `ReadOnlySpan<T>.IndexOf()`.

## Methods 
The following **Extension Methods** are contained: 
 
#### String Methods made available for **`ReadonlySpan<T>`** and **`Span<T>`**:
  
- `(ReadOnly-)Span<T>.Split(T delimiter)`
- `(ReadOnly-)Span<T>.Split(T delimiter, int count)`
- `(ReadOnly-)Span<T>.Split(T delimiter, StringSplitOptions options)` 
- `(ReadOnly-)Span<T>.Split(T delimiter, StringSplitOptions options, int count)`
- `(ReadOnly-)Span<T>.Split(ReadOnlySpan<T> delimiters)`
- `(ReadOnly-)Span<T>.Split(ReadOnlySpan<T> delimiters, int count)`
- `(ReadOnly-)Span<T>.Split(ReadOnlySpan<T> delimiters, StringSplitOptions options)` 
- `(ReadOnly-)Span<T>.Split(ReadOnlySpan<T> delimiters, StringSplitOptions options, int count)`
- `(ReadOnly-)Span<T>.SplitAny(ReadOnlySpan<T> delimiters)`
- `(ReadOnly-)Span<T>.SplitAny(ReadOnlySpan<T> delimiters, int count)`
- `(ReadOnly-)Span<T>.SplitAny(ReadOnlySpan<T> delimiters, StringSplitOptions options)` 
- `(ReadOnly-)Span<T>.SplitAny(ReadOnlySpan<T> delimiters, StringSplitOptions options, int count)`
- `(ReadOnly-)Span<T>.Remove(int startIndex)`
- `Span<T>.Replace(T oldT, T newT)`

#### Linq Methods made available for **`ReadonlySpan<T>`** and **`Span<T>`**:

- `(ReadOnly-)Span<T>.All(Predicate<T> predicate)` 
- `(ReadOnly-)Span<T>.Any(Predicate<T> predicate)` 
- `(ReadOnly-)Span<T>.Average()` 
- `(ReadOnly-)Span<T>.Sum()`  
- `(ReadOnly-)Span<T>.Skip(int count)` 
- `(ReadOnly-)Span<T>.Take(int count)`
- `(ReadOnly-)Span<T>.SkipLast(int count)` 
- `(ReadOnly-)Span<T>.Takelast(int count)`
- `(ReadOnly-)Span<T>.SkipWhile(Predicate<T> condition)` 
- `(ReadOnly-)Span<T>.TakeWhile(Predicate<T> condition)`
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
- `(Readonly-)Span<T>.Min()`
- `(Readonly-)Span<T>.Min(Func<TSource, TResult> selector)`
- `(Readonly-)Span<T>.MinBy(Func<TSource, TKey> keySelector)`
- `(Readonly-)Span<T>.MinBy(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)`
- `(Readonly-)Span<T>.Max()`
- `(Readonly-)Span<T>.Max(Func<TSource, TResult> selector)`
- `(Readonly-)Span<T>.MaxBy(Func<TSource, TKey> keySelector)`
- `(Readonly-)Span<T>.MaxBy(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)`

## Contributing

Thank you for your interest in contributing to this project - Please see [Contributing](CONTRIBUTING.md)!
## License

Copyright (c) draconware-dev. All rights reserved. 

Licensed under the [MIT](../LICENSE) License.
