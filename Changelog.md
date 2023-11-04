# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


### Fixed 

- Grammatical Issues in some documentation comments 
- Broken link to the repository on nuget (#3)