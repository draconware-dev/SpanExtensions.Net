# SpanExtensions

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

## Contributing

Thank you for your interest in contributing to this project! - You may contribute on [Github](https://github.com/draconware-dev/SpanExtensions.Net).  
## License

Copyright (c) draconware-dev. All rights reserved. 

Licensed under the [MIT](https://github.com/draconware-dev/SpanExtensions.Net/blob/main/LICENSE) license.
