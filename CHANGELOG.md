# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/).

## [2.0.0] - 2026-04-16

### Removed
- Extensions.IsSomeAnd that test for TValue. Where obsolete since 1.1.0 (#13)

### Depricated
- Extensions.IsSomeAnd that test for Predicate. Use `TextExtensions.ShouldBeSome()` for assertions, and `Reduce()` or `Match()` when closing the `Option<>` Monad. (#13)
- Extensions.OrElse. Use `Reduce()` for normal use cases, convert back to `Option.Some` if nessesary. (#17)
- Option.Try. Use `Result.Try` from `Toarnbeike.Results` to retain failure information. (#13)
- TestExtensions.ShouldBeSomeWithValue(). Since TestExtensions is not meant as assertion library. Use `ShouldBeSome()` followed by Shouldly assertions. (#13)
- TestExtensions.ShouldBeSomeThatMatches(). Since TestExtensions is not meant as assertion library. Use `ShouldBeSome()` followed by Shouldly assertions. (#13)
- TestExtensions.ShouldBeSomeThatSatisfies(). Since TestExtensions is not meant as assertion library. Use `ShouldBeSome()` followed by Shouldly assertions. (#13)

### Changed
- CollectionExtensions.LastOrNone use optimized path for IList and IReadOnlyList (#1)
- Cleaned README.md and moved linq description to separate doc (#15)

### Tooling
- Migrated to .slnx solution file (#14)

## [1.1.0] - 2025-12-31

### Added
- TestExtensions are integrated directly into the package.

### Changed
- Option is now a readonly record struct for lighter memory footprint.
- None is added as an explicit type, useful for conversions to `Union<TValue, None>`
- ConfigureAwait(false) added for all async 
- Tap and TapIfNone now return the original `Option`, to allow for further chaining.
- Updated to `.Net10`
- Added Linq query language support (from ... in ... where ... select)
- Updated test suite to use TUnit

### Deprecated
- Toarnbeike.Optional.TestExtensions nuget package are now deprecated, 
  Use the integrated TestExtensions (`using Toarnbeike.Optional.TestExtension`) from this package. 
- IsSomeAnd overloads for a single value are removed.
  - Use `TestExtensions.ShouldBeSome()` for assertions.
  - Use `IsSomeAnd(Func<TValue, bool>)` for generic filtering on Option.

### Tooling
- Added changelog
- Start using Directory.Packages.props for Central package management
- Improved CI/CD 

---

## [1.0.1] - 2025-07-25

### Added
- Initial release of `Toarnbeike.Optional`
- Fluent API for working with optional values
- Implicit conversion from values and `Option.None`
- Extension methods inspired by F#, Rust and Haskell
- Full support for `Task<Option<T>>` async scenarios
- Rich LINQ-style extensions for `IEnumerable<Option<T>>`
- Assertion syntax similar to Shouldly for test projects