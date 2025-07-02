using BenchmarkDotNet.Attributes;
using Vectorial1024.Collections.Generic;

namespace DictionaryListBenchmarking;

[MemoryDiagnoser(false)]
public class DictListBenchmarks
{
    [Params(10, 100, 1000, 10000, 100000)]
    public int N;

    private List<int> _masterList = [];

    private Dictionary<int, int> _masterDict = new();

    private SortedDictionary<int, int> _masterSortedDict = new();

    private DictionaryList<int> _masterDictList = new();

    private List<int> _iterList = [];

    private Dictionary<int, int> _iterDict = new();

    private SortedDictionary<int, int> _iterSortedDict = new();

    private DictionaryList<int> _iterDictList = new();

    [GlobalSetup]
    public void PopulateStuff()
    {
        // populate with numbers from 0 to N-1

        _masterList.Clear();
        _masterList.TrimExcess();
        _masterDict.Clear();
        _masterDict.TrimExcess();
        _masterSortedDict.Clear();
        _masterDictList.Clear();
        _masterDictList.CompactAndTrimExcess();

        for (var i = 0; i < N; i++)
        {
            _masterList.Add(i);
            _masterDict.Add(i, i);
            _masterSortedDict.Add(i, i);
            _masterDictList.Add(i);
        }
    }

    [IterationSetup]
    public void CloneStuff()
    {
        _iterList = new List<int>(_masterList);
        _iterDict = new Dictionary<int, int>(_masterDict);
        _iterSortedDict = new SortedDictionary<int, int>(_masterSortedDict);
        _iterDictList = new DictionaryList<int>(_masterDictList);
    }

    #region AppendMany

    [Benchmark]
    public void AppendManyToList()
    {
        var list = new List<int>();
        for (var i = 0; i < N; i++)
        {
            list.Add(i);
        }
    }

    [Benchmark]
    public void AppendManyToDict()
    {
        var dict = new Dictionary<int, int>();
        for (var i = 0; i < N; i++)
        {
            dict[i] = i;
        }
    }

    [Benchmark]
    public void AppendManyToSortedDict()
    {
        var sortedDict = new SortedDictionary<int, int>();
        for (var i = 0; i < N; i++)
        {
            sortedDict[i] = i;
        }
    }

    [Benchmark]
    public void AppendManyToDictList()
    {
        var dictList = new DictionaryList<int>();
        for (var i = 0; i < N; i++)
        {
            dictList.Add(i);
        }
    }

    #endregion

    #region Iterate

    [Benchmark]
    public void IterateList()
    {
        var sum = 0L;
        foreach (var num in _masterList)
        {
            sum += num;
        }
    }

    [Benchmark]
    public void IterateDict()
    {
        var sum = 0L;
        foreach (var kv in _masterDict)
        {
            sum += kv.Value;
        }
    }

    [Benchmark]
    public void IterateSortedDict()
    {
        var sum = 0L;
        foreach (var kv in _masterSortedDict)
        {
            sum += kv.Value;
        }
    }

    [Benchmark]
    public void IterateDictList()
    {
        var sum = 0L;
        foreach (var kv in _masterDictList)
        {
            sum += kv.Value;
        }
    }

    #endregion

    #region ReadMany

    [Benchmark]
    public void ReadManyFromList()
    {
        // access all indexes of multiples of 5
        for (var i = 0; i < N; i += 5)
        {
            _ = _iterList[i];
        }
    }

    [Benchmark]
    public void ReadManyFromDict()
    {
        // access all indexes of multiples of 5
        for (var i = 0; i < N; i += 5)
        {
            _ = _iterDict[i];
        }
    }

    [Benchmark]
    public void ReadManyFromSortedDict()
    {
        // access all indexes of multiples of 5
        for (var i = 0; i < N; i += 5)
        {
            _ = _iterSortedDict[i];
        }
    }

    [Benchmark]
    public void ReadManyFromDictList()
    {
        // access all indexes of multiples of 5
        for (var i = 0; i < N; i += 5)
        {
            _ = _iterDictList[i];
        }
    }

    #endregion

    #region RemoveMany

    [Benchmark]
    public void RemoveManyFromListInPlace()
    {
        // remove everything not divisible by 6
        for (var i = _iterList.Count - 1; i >= 0; i--)
        {
            if (i % 6 == 0)
            {
                continue;
            }
            _iterList.RemoveAt(i);
        }
    }

    [Benchmark]
    public void RemoveManyFromListWithLinq()
    {
        // remove everything not divisible by 6
        _iterList = _iterList.Where(i => i % 6 == 0).ToList();
    }

    [Benchmark]
    public void RemoveManyFromDictInPlace()
    {
        // remove everything not divisible by 6
        foreach (var kv in _iterDict)
        {
            if (kv.Key % 6 == 0)
            {
                continue;
            }
            _iterDict.Remove(kv.Key);
        }
    }

    [Benchmark]
    public void RemoveManyFromDictWithLinq()
    {
        // remove everything not divisible by 6
        _iterDict = _iterDict.Where(kv => kv.Key % 6 == 0).ToDictionary();
    }

    [Benchmark]
    public void RemoveManyFromSortedDictInPlace()
    {
        // remove everything not divisible by 6
        foreach (var kv in _iterSortedDict)
        {
            if (kv.Key % 6 == 0)
            {
                continue;
            }
            _iterDict.Remove(kv.Key);
        }
    }

    [Benchmark]
    public void RemoveManyFromSortedDictWithLinq()
    {
        // remove everything not divisible by 6
        var temp = _iterDict.Where(kv => kv.Key % 6 == 0).ToDictionary();
        var target = new SortedDictionary<int, int>();
        foreach (var kv in temp)
        {
            target.Add(kv.Key, kv.Value);
        }
        _iterSortedDict = target;
    }

    [Benchmark]
    public void RemoveManyFromDictListInPlace()
    {
        // remove everything not divisible by 6
        foreach (var kv in _iterDictList)
        {
            if (kv.Key % 6 == 0)
            {
                continue;
            }
            _iterDictList.UnsetAt(kv.Key);
        }
    }

    [Benchmark]
    public void RemoveManyFromDictListWithLinq()
    {
        // remove everything not divisible by 6
        var temp = _iterDictList.Where(kv => kv.Key % 6 == 0).ToList();
        var target = new DictionaryList<int>(temp.Count);
        foreach (var kv in target)
        {
            target.Add(kv.Value);
        }
        _iterDictList = target;
    }

    #endregion
}
