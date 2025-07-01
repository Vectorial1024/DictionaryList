# Change Log of `DictionaryList`
This package follows semantic versioning. When in doubt, refer to the latest docstrings.

## Dev (WIP)
Initial release.

(Important note: `0.1.0` is the "let's see how NuGet works" release, while `0.1.1` polishes the package more by fixing bugs, etc.)  

The `DictionaryList<T>` is essentially a `List<T>` that allows deferred reindexing when adding/removing items. See the readme for more info.

This version is currently minimally viable. It works, but advanced convenience methods (e.g. `AddRange()`) are not available yet.
Further optimization and refinement can still be made.

### Sub-release 0.1.1:
- Fixed calling `DictionaryList<T>.Clear()` does nto result in correct `DictionaryList<T>.Count`
- Reading the `Dictionary<T>` with an unset index now throws `KeyNotFoundException` instead of `ArgumentOutOfRangeException`
