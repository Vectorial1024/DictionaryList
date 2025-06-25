# DictionaryList
(Badges, etc)

When PHP arrays meet C#: high-performance reinterpretation of Lists.

## Situation
Consider a situation where a `List<T>` needs to receive and remove many items. We quickly run into problems:
- Removing many items from `List<T>` is generally painfully slow!
- Removing items from `List<T>` changes all existing index values (in case they are important)
- Removing items from `List<T>` inside a `foreach` block is not allowed!

Here, it is obvious `List<T>` is a bad choice. But, also consider the alternatives:
- `Array<T>` does not automatically resize: tedious memory management
- It is very slow to insert many data into `Dictionary<int,T>`
- `HashSet<T>` rejects duplicated data (perhaps your data may contain duplicates), and is basically a fancy `Dictionary<T,void>`
- LINQ expressions are usually the fastest, but will require extra memory allocation when building the solution `List<T>`
- Or, perhaps the problem asks for a `foreach` solution with side effects, such that LINQ is not suitable

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
- Implement the various list interfaces
- Performance optimization

## Characteristics of `DictionaryList<TValue>`
Consider the following primer table:

|                      | List Behavior             | Dictionary Behavior      |
|----------------------|---------------------------|--------------------------|
| List Structure       | `List<T>`                 | `ListDictionary<int, T>` |
| Dictionary Structure | 👉 `DictionaryList<T>` 👈 | `Dictionary<int, T>`     |

A `DictionaryList<T>` is a `List<T>` that has `Dictionary`-like structure.
Even if you happen to know about the PHP `array` type already, it is still a good idea to go through this section, 
because `DictionaryList<T>` is still a bit different from the usual PHP `array`.

Characteristics of a `DictionaryList<T>`:
- `int` keys only, same as a List
- NO "inserting" items in the middle of the list
  - Appending at the end is still allowed
- Removing items DOES NOT reindex elements, but will leave behind "gaps"
  - Use `CompactAndTrimExcess()` to reindex elements and reclaim these gaps
- CANNOT read from out-of-bounds indexes or removed indexes
  - Use `ContainsIndex()` to check whether an index is readable
- Traversal yields `KeyValuePair<int,T>`
  - Traversal uses ascending `int` order 

Perhaps this can be better demonstrated with some code sample.

## Usage
As stated, you should expect an API which is similar to a `List<T>` (see the docstring for the latest info).
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
dictList.Unset(1);
_ = dictList.ContainsIndex(1); // false
_ = dictList.Count; // 4
// _ = dictList[1]; // out of bounds; not allowed!

// "revive" indexes, but we recommend Add() if you do not care about the value of indexes.
dictList[1] = 11;
_ = dictList[1]; // 11;

// let's delete it again...
dictList.Unset(1);

// ...to demonstrate traversal
foreach (var kv in dictList) 
{
    _ = kv.Key;
    _ = kv.Value;
    // yields in order: (0, 1), (2, 3), (3, 12), (4, 5); 4 items!
}

// we have a gap at index=1; reclaim gaps by doing:
dictList.CompactAndTrimExcess();

// we are done with the data; clear everything by doing:
dictList.Clear();
_ = dictList.Count; // 0
```

### Thread Safety
`DictionaryList<T>` is NOT thread safe!

## Benchmarking
This package is benchmarked with the BenchmarkDotNet package.

To run the benchmark:

```shell
# BenchmarkDotNet strongly recommends benchmarking in Release mode
dotnet run -c=Release --project=Benchmarking
```

### Quick performance comparison between relevant collection types
| Task                                | List   | DictionaryList | Dictionary |
|-------------------------------------|--------|----------------|------------|
| Append Many Items (speed)           | 👍     | 👌             | 👎👎       |
| Append Many Items (memory)          | 👍     | 👌             | 👎👎       |
| Full Iteration (speed)              | 👍     | 👎             | 👌         |
| Full Iteration (memory)             | 👍 (0) | 👌             | 👍 (0)     |
| Read Many Items (speed)             | 👍     | 👌             | 👎         |
| Read Many Items (memory)            | 👍 (0) | 👍 (0)         | 👍 (0)     |
| Remove Many Items In-place (speed)  | 👎👎👎 | 👌             | 👍         |
| Remove Many Items In-place (memory) | 👍 (0) | 👌             | 👍 (0)     |
| Remove Many Items w/ LINQ (speed)   | 👍     | 👌             | 👎👎       |
| Remove Many Items w/ LINQ (memory)  | 👍     | 👌             | 👎         |
| Emit Key/Index During `foreach`     | ❌      | ✔️             | ✔️         |

You may see that `DictionaryList<T>` is an all-rounded, midway solution between a `List<T>` and a `Dictionary<TKey,TValue>`. 

### Sample benchmarking results
See `BENCHMARK.md`.

## Testing
This package uses NUnit for testing.

Run the tests with:

```shell
dotnet test
```
