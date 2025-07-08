# DictionaryList
[![GitHub License][github-license-image]][github-repo-url]
[![NuGet Version][nuget-version-image]][nuget-url]
[![NuGet Downloads][nuget-downloads-image]][nuget-stats-url]
[![GitHub Actions Workflow Status][cs-build-status-image]][github-actions-url]
[![GitHub Repo Stars][github-stars-image]][github-repo-url]
[![GitHub Sponsors][github-sponsors-image]][github-sponsors-url]

When PHP arrays meet C#: an all-rounded reinterpretation of Lists.

## Situation
Consider a task that uses a `List<T>`:
- Receive/append many items
- Remove many elements *in place*: to conserve memory, in case your list is large

We quickly notice problems when removing many items in-place from `List<T>`:
- This is actually `O(n^2)`! (where n = number of items in the list)
- Can't keep old indexes (perhaps we need them for later)
- Can't do that in a `foreach` loop

We also notice the problems of the alternatives:
- `Array<T>` does not automatically resize: tedious memory management
- It is very slow to insert many data into `Dictionary<int,T>`
  - By the way, `HashSet<T>` is essentially equivalent to `Dictionary<T,void>`
- LINQ expressions are usually the fastest, but will require extra memory allocation when building the solution `List<T>`
- Or, perhaps the problem asks for a `foreach` solution with side effects, such that LINQ is not suitable

This is a bad situation between a rock and a hard place.

Inspired by PHP's highly unconventional approach to implement arrays, lists and maps,
this C# package introduces a hybrid between the `List<T>` type and the `Dictionary<TKey,TValue>` type: the `DictionaryList<T>` type.

Do not be afraid of the PHP origin.
While there are some trade-offs when migrating from `List<T>` to `DictionaryList<T>` (see the Characteristics and the Benchmarking sections),
rest assured that `DictionaryList<T>` really works well.

## Install
via NuGet:

```shell
dotnet add package Vectorial1024.DictionaryList
```

### Change log
Please see `CHANGELOG.md`.

### Todos
Some todos of this package:
- Implement the various list interfaces
- Performance optimization

## Characteristics of `DictionaryList<TValue>`
Consider the following primer table:

|                      | List Behavior             | Dictionary Behavior      |
|----------------------|---------------------------|--------------------------|
| List Structure       | `List<T>`                 | `ListDictionary<int, T>` |
| Dictionary Structure | 👉 `DictionaryList<T>` 👈 | `Dictionary<int, T>`     |

A `DictionaryList<T>` is a `List<T>` that has `Dictionary`-like structure.

The theme of a `DictionaryList<T>` is "deferred reindexing".
Compared to a `List<T>`, there are some characteristics/restrictions:
- No `Insert()`; use `Append()` instead
- No `RemoveAt()`; use `UnsetAt()` instead, which leaves behind "memory gaps"
- Memory gaps may be reused if you know their indexes; also see `ContainsIndex()`
- Enumeration yields `KeyValuePair<int,T>` and skips over unset rows
- No `TrimExcess()`; use `CompactAndTrimExcess()` instead
- `UnsetAt()` during `foreach` is allowed!

Perhaps this can be better demonstrated with some code sample.

## Usage
The API is similar to a `List<T>` (see the docstring or your IDE for the latest info).
Still, you may consider the following sample code:

```csharp
// basic usage sample code

// create a new DictionaryList
var dictList = new DictionaryList<int>;

// add some items to it
dictList.Add(1); // index 0
dictList.Add(2); // index 1
dictList.Add(3); // index 2
dictList.Add(4); // index 3
dictList.Add(5); // index 4

// how many items?
_ = dictList.Count; // 5

// read from it
_ = dictList.ContainsIndex(3); // true
_ = dictList[3]; // 4

// write to it
dictList[3] = 12;
_ = dictList[3]; // 12
// dictList[99] = 99999; // out of bounds; not allowed!
// dictList[-4] = 42; // out of bounds; not allowed!

// remove from it
// note: removed items can be GC-ed if that was the last reference to it
dictList.UnsetAt(1);
_ = dictList.ContainsIndex(1); // false
_ = dictList.Count; // 4
// _ = dictList[1]; // out of bounds; not allowed!

// "revive" indexes, but we recommend Add() if you do not care about the value of indexes.
dictList[1] = 11;
_ = dictList[1]; // 11;

// let's delete it again...
dictList.UnsetAt(1);

// ...to demonstrate traversal
foreach (var kv in dictList) 
{
    _ = (kv.Key, kv.Value);
    // yields in order: (0, 1), (2, 3), (3, 12), (4, 5); 4 items!
}

// we have a gap at index=1; reclaim gaps by doing:
dictList.CompactAndTrimExcess();

// we are done with the data; clear everything by doing:
dictList.Clear();
_ = dictList.Count; // 0
```

### Thread Safety?
`DictionaryList<T>` is NOT thread safe!

### Native AOT?
`DictionaryList<T>` is compatible with Native AOT.

## Benchmarking
This package is benchmarked with the BenchmarkDotNet package.

To run the benchmark:

```shell
# BenchmarkDotNet strongly recommends benchmarking in Release mode
dotnet run -c=Release --project=Benchmarking
```

### Quick performance comparison between relevant collection types
A benchmark was run with version `0.1.2` of this library. Its detailed results can be found in `BENCHMARK.md`.

But still, here is a table outlining the performances when `N = 100000`:

| Task                                | List        | DictionaryList | Dictionary | SortedDictionary |
|-------------------------------------|-------------|----------------|------------|------------------|
| Append Many Items (speed)           | 155 ms ⭐    | 317 ms 👌      | 1073 ms    | 10507 ms         |
| Append Many Items (memory)          | 1048976 B ⭐ | 2097536 B 👌   | 6037640 B  | 4800112 B        |
| Full Iteration (speed)              | 50 ms ⭐     | 176 ms 👌      | 162 ms     | 587 ms           |
| Full Iteration (memory)             | 0 B         | 48 B           | 0 B        | 312 B            |
| Read Many Items (speed)             | 17 ms ⭐     | 65 ms 👌       | 147 ms     | 1322 ms          |
| Read Many Items (memory)            | 0 B         | 0 B            | 0 B        | 0 B              |
| Remove Many Items In-place (speed)  | 557307 ms   | 789 ms 👌      | 316 ms ⭐   | 810 ms           |
| Remove Many Items In-place (memory) | 0 B         | 48 B           | 0 B        | 312 B            |
| Remove Many Items w/ LINQ (speed)   | 104 ms ⭐    | 793 ms 👌      | 885 ms     | 1693 ms          |
| Remove Many Items w/ LINQ (memory)  | 198072 B ⭐  | 529280 B 👌    | 673168 B   | 1473296 B        |
| Add Items During `foreach`          | ❌           | ❌ new key      | ❌ new key  | ❌                |
| Emit Key/Index During `foreach`     | 🤷 [1]      | ✔️             | ✔️         | ✔️               |
| Replace Items During `foreach`      | 🤷 [1]      | ✔️             | ✔️         | ❌ if key exists  |
| Remove Items During `foreach`       | ❌           | ✔️             | ✔️         | ❌                |

You may see that `DictionaryList<T>` is an all-rounded, midway solution between a `List<T>` and a `Dictionary<TKey,TValue>`.

As part of the benchmark, you may also see that `SortedDictionary<TKey,TValue>` is generally a bad type to use compared to other similar types.

The following collection types are excluded from the benchmarking:
- `HashSet<T>`: too similar to `Dictionary<T, void>`, probably has similar performance
- `OrderedDictionary`: ambiguity between intended `<int, T>` usage and `this[int]` syntax
- `LinkedList<T>`: unfair/impossible comparison due to lack of index access
- `SortedList<int,T>`: too similar to `SortedDictionary<int, T>`, probably has similar performance

## Testing
This package uses NUnit for testing.

Run the tests with:

```shell
dotnet test
```

## Appendix
[1] Technically, this can be done with Enumerable LINQ's `Index` method, but using LINQ with `foreach` is perhaps an antipattern. 

[nuget-url]: https://www.nuget.org/packages/Vectorial1024.DictionaryList/
[nuget-stats-url]: https://www.nuget.org/stats/packages/Vectorial1024.DictionaryList?groupby=Version
[github-repo-url]: https://github.com/Vectorial1024/DictionaryList
[github-actions-url]: https://github.com/Vectorial1024/DictionaryList/actions/workflows/dotnet.yml
[github-sponsors-url]: https://github.com/sponsors/Vectorial1024

[github-license-image]: https://img.shields.io/github/license/Vectorial1024/DictionaryList?style=plastic
[nuget-version-image]: https://img.shields.io/nuget/v/Vectorial1024.DictionaryList?style=plastic
[nuget-downloads-image]: https://img.shields.io/nuget/dt/Vectorial1024.DictionaryList?style=plastic
[cs-build-status-image]: https://img.shields.io/github/actions/workflow/status/Vectorial1024/DictionaryList/dotnet.yml?style=plastic
[github-stars-image]: https://img.shields.io/github/stars/vectorial1024/DictionaryList
[github-sponsors-image]: https://img.shields.io/github/sponsors/Vectorial1024?style=plastic
