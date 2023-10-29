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

## Contributing

Thank you for your interest in contributing to this project!
## License

Copyright (c) draconware-dev. All rights reserved. 

Licensed under the [MIT](LICENSE) license.
# Performance   
| Method       | Size | Mean         | Error      | StdDev     | Allocated | Ratio |
|------------- |----- |-------------:|-----------:|-----------:|----------:|------:|
| All_Linq     | 10   |    88.814 ns |  0.5532 ns |  0.5174 ns |      32 B |   1.00|
| All_Array    | 10   |    27.090 ns |  0.1771 ns |  0.1657 ns |         - |   0.31|
| All_Span     | 10   |    27.374 ns |  0.1493 ns |  0.1247 ns |         - |   0.31|
| All_SIMD_128 | 10   |     7.944 ns |  0.0362 ns |  0.0321 ns |         - |   0.09|
| All_SIMD_256 | 10   |     6.587 ns |  0.0049 ns |  0.0041 ns |         - |   0.07|
| All_Linq     | 100  |   650.510 ns |  4.1553 ns |  3.6835 ns |      32 B |   1.00|
| All_Array    | 100  |   264.005 ns |  1.2532 ns |  1.1723 ns |         - |   0.41|
| All_Span     | 100  |   263.289 ns |  0.1447 ns |  0.1283 ns |         - |   0.40|
| All_SIMD_128 | 100  |    53.195 ns |  0.3149 ns |  0.2946 ns |         - |   0.08|
| All_SIMD_256 | 100  |    30.652 ns |  0.0911 ns |  0.0807 ns |         - |   0.05|
| All_Linq     | 1000 | 6,172.586 ns | 38.6840 ns | 36.1851 ns |      32 B |   1.00|
| All_Array    | 1000 | 2,591.976 ns | 22.9460 ns | 21.4637 ns |         - |   0.42|
| All_Span     | 1000 | 2,594.871 ns | 20.5133 ns | 19.1882 ns |         - |   0.42|
| All_SIMD_128 | 1000 |   527.125 ns |  6.3450 ns |  5.9351 ns |         - |   0.09|
| All_SIMD_256 | 1000 |   262.203 ns |  4.0576 ns |  3.7955 ns |         - |   0.04|
