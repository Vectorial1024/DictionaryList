# DictionaryList
(Badges, etc)

When PHP arrays meet C#: high-performance reinterpretation of Lists.

## Situation
Consider a situation where a `List<T>` needs to receive and remove many items. We quickly run into problems:
- Removing items from `List<T>` is generally painfully expensive!
- Removing items from `List<T>` changes all existing index values (in case they are important)

It is obvious `List<T>` is a bad choice. But, also consider the alternatives:
- `Array<T>` does not automatically resize: tedious memory management
- It is very expensive to insert many data into `Dictionary<int, T>`
- `HashSet<T>` rejects duplicated data (perhaps your data may contain duplicates)
- `Queue<T>` does not allow direct element access
- Sometimes, using `IEnumerator` to lazily load/find items could be slow
- Or, perhaps batch processing is required so that `IEnumerator` is not suitable

This is a bad situation between a rock and a hard place.

Inspired by PHP's highly unconventional approach to implement arrays, lists and maps,
this C# package introduces a hybrid between the `List<T>` type and the `Dictionary<TKey, TValue>` type: the `DictionaryList<T>` type.

Do not be afraid of the PHP origin.
While there are some trade-offs when migrating from `List<T>` to `DictionaryList<T>` (see the Characteristics and the Benchmarking sections),
rest assured that `DictionaryList<T>` really works well.

## Install
via NuGet:

(WIP)

### Change log
Please see `CHANGELOG.md`.

### Todos
Some todos of this package:
- Test cases
- Implement the various list interfaces
- Performance optimization

## Characteristics of `DictionaryList<TValue>`
Consider the following primer table:

|                      | List Behavior             | Dictionary Behavior |
|----------------------|---------------------------|---------------------|
| List Structure       | `List<T>`                 | `ListDictionary<T>` |
| Dictionary Structure | 👉 `DictionaryList<T>` 👈 | `Dictionary<T>`     |

A `DictionaryList<T>` is a `List<T>` that has `Dictionary`-like behavior.
Even if you happen to know about the PHP `array` type already, it is still a good idea to go through this section, 
because `DictionaryList<T>` is still a bit different from the usual PHP `array`.

(WIP)

## Usage
As stated, you should expect an API which is similar to a `List<T>` (see the docstring for the latest info).
Still, you may consider the following sample code:

```csharp
// wip
```

## Benchmarking
This package is benchmarked with the BenchmarkDotNet package.

To run the benchmark:

```shell
# BenchmarkDotNet strongly recommends benchmarking in Release mode
dotnet build -c=Release
dotnet run -c=Release --project=Benchmarking
```

### Quick performance comparison between relevant collection types
| Task                              | List   | DictionaryList | Dictionary |
|-----------------------------------|--------|----------------|------------|
| Add Many Items (speed)            | 👍     | 👌             | 👎👎       |
| Add Many Items (memory)           | 👍     | 👌             | 👎👎       |
| Full iteration (speed)            | 👍     | 👎             | 👌         |
| Full iteration (memory)           | 👍 (0) | 👌             | 👍 (0)     |
| Read Many Items (speed)           | 👍     | 👌             | 👎         |
| Read Many Items (memory)          | 👍 (0) | 👍 (0)         | 👍 (0)     |
| Remove Many Items + Trim (speed)  | 👎👎👎 | 👌             | 👍         |
| Remove Many Items + Trim (memory) | 👍 (0) | 👌             | 👍 (0)     |

You may see that `DictionaryList<T>` is an all-rounded, midway solution between a `List<T>` and a `Dictionary<TKey,TValue>`. 

### Sample benchmarking results
See `BENCHMARK.md`.

## Testing
(WIP)
