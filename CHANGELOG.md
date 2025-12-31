# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.1.0] - Unreleased

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