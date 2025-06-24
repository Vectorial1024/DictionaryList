```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4351/24H2/2024Update/HudsonValley)
13th Gen Intel Core i7-13700HX 2.10GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 9.0.300
  [Host]     : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2
  Job-CNUJVU : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method                            | N      | Mean             | Error           | StdDev          | Median           | Allocated |
|-----------------------------------|------- |-----------------:|----------------:|----------------:|-----------------:|----------:|
| **AppendManyToList**              | **10**     |       **1,172.2 ns** |        **89.79 ns** |       **260.51 ns** |       **1,200.0 ns** |     **216 B** |
| AppendManyToDict                  | 10     |       1,438.1 ns |       115.77 ns |       335.86 ns |       1,400.0 ns |     776 B |
| AppendManyToDictList              | 10     |       1,357.4 ns |        71.31 ns |       203.46 ns |       1,300.0 ns |     360 B |
| IterateList                       | 10     |         310.2 ns |        28.06 ns |        81.85 ns |         300.0 ns |         - |
| IterateDict                       | 10     |         251.0 ns |        23.22 ns |        67.72 ns |         200.0 ns |         - |
| IterateDictList                   | 10     |       1,694.9 ns |       103.37 ns |       303.17 ns |       1,700.0 ns |      40 B |
| ReadManyFromList                  | 10     |         134.3 ns |        22.93 ns |        67.25 ns |         100.0 ns |         - |
| ReadManyFromDict                  | 10     |         275.8 ns |        32.71 ns |        95.93 ns |         300.0 ns |         - |
| ReadManyFromDictList              | 10     |         115.3 ns |        26.67 ns |        77.79 ns |         100.0 ns |         - |
| RemoveManyFromListThenTrim        | 10     |         491.0 ns |        47.74 ns |       140.77 ns |         500.0 ns |         - |
| RemoveManyFromDictThenTrim        | 10     |         430.9 ns |        33.88 ns |        98.28 ns |         400.0 ns |         - |
| RemoveManyFromDictListThenCompact | 10     |       1,943.9 ns |        91.10 ns |       267.19 ns |       1,950.0 ns |      40 B |
| **AppendManyToList**              | **100**    |       **1,885.7 ns** |       **100.64 ns** |       **293.57 ns** |       **1,900.0 ns** |    **1184 B** |
| AppendManyToDict                  | 100    |       2,843.4 ns |       136.05 ns |       399.02 ns |       2,800.0 ns |    7392 B |
| AppendManyToDictList              | 100    |       2,469.4 ns |        89.07 ns |       259.82 ns |       2,400.0 ns |    2224 B |
| IterateList                       | 100    |       1,034.5 ns |        32.88 ns |        90.00 ns |       1,000.0 ns |         - |
| IterateDict                       | 100    |       1,081.6 ns |        53.83 ns |       157.03 ns |       1,050.0 ns |         - |
| IterateDictList                   | 100    |       4,899.0 ns |       170.13 ns |       496.27 ns |       4,750.0 ns |      40 B |
| ReadManyFromList                  | 100    |         223.0 ns |        22.01 ns |        64.91 ns |         200.0 ns |         - |
| ReadManyFromDict                  | 100    |         342.4 ns |        28.44 ns |        83.41 ns |         300.0 ns |         - |
| ReadManyFromDictList              | 100    |         414.1 ns |        25.32 ns |        74.24 ns |         400.0 ns |         - |
| RemoveManyFromListThenTrim        | 100    |       2,195.8 ns |       111.88 ns |       322.79 ns |       2,100.0 ns |         - |
| RemoveManyFromDictThenTrim        | 100    |       1,964.6 ns |        49.27 ns |       144.50 ns |       2,000.0 ns |         - |
| RemoveManyFromDictListThenCompact | 100    |       5,668.4 ns |       117.29 ns |       298.53 ns |       5,700.0 ns |      40 B |
| **AppendManyToList**              | **1000**   |       **8,282.8 ns** |       **432.00 ns** |     **1,225.51 ns** |       **8,000.0 ns** |    **8424 B** |
| AppendManyToDict                  | 1000   |      14,544.7 ns |       445.75 ns |     1,271.75 ns |      14,450.0 ns |   73168 B |
| AppendManyToDictList              | 1000   |      13,526.1 ns |       330.13 ns |       931.13 ns |      13,500.0 ns |   16632 B |
| IterateList                       | 1000   |       6,933.0 ns |       491.17 ns |     1,401.33 ns |       6,650.0 ns |         - |
| IterateDict                       | 1000   |       8,945.9 ns |       177.06 ns |       444.20 ns |       8,900.0 ns |         - |
| IterateDictList                   | 1000   |      39,831.3 ns |     1,148.95 ns |     3,066.77 ns |      39,300.0 ns |      40 B |
| ReadManyFromList                  | 1000   |       1,675.0 ns |        34.02 ns |        44.23 ns |       1,700.0 ns |         - |
| ReadManyFromDict                  | 1000   |       2,174.0 ns |       148.39 ns |       437.54 ns |       2,000.0 ns |         - |
| ReadManyFromDictList              | 1000   |       2,164.0 ns |       139.30 ns |       410.72 ns |       1,950.0 ns |         - |
| RemoveManyFromListThenTrim        | 1000   |      22,200.0 ns |       783.05 ns |     2,221.39 ns |      21,500.0 ns |         - |
| RemoveManyFromDictThenTrim        | 1000   |      16,528.3 ns |       349.57 ns |       985.98 ns |      16,450.0 ns |         - |
| RemoveManyFromDictListThenCompact | 1000   |      46,674.1 ns |       924.85 ns |     1,950.83 ns |      46,500.0 ns |      40 B |
| **AppendManyToList**              | **10000**  |      **17,263.0 ns** |       **575.15 ns** |     **1,622.22 ns** |      **16,950.0 ns** |  **131400 B** |
| AppendManyToDict                  | 10000  |      89,788.8 ns |     2,814.94 ns |     8,211.32 ns |      90,800.0 ns |  673064 B |
| AppendManyToDictList              | 10000  |      30,840.1 ns |       751.88 ns |     2,108.35 ns |      30,450.0 ns |  262488 B |
| IterateList                       | 10000  |      10,607.4 ns |       381.06 ns |     1,087.19 ns |      10,300.0 ns |         - |
| IterateDict                       | 10000  |      15,508.8 ns |       337.55 ns |       946.53 ns |      15,300.0 ns |         - |
| IterateDictList                   | 10000  |      52,957.9 ns |       993.69 ns |     1,714.07 ns |      52,550.0 ns |      40 B |
| ReadManyFromList                  | 10000  |       6,444.9 ns |       418.56 ns |     1,159.84 ns |       6,100.0 ns |         - |
| ReadManyFromDict                  | 10000  |      11,723.7 ns |       540.02 ns |     1,531.94 ns |      11,200.0 ns |         - |
| ReadManyFromDictList              | 10000  |       9,546.2 ns |       516.85 ns |     1,449.31 ns |       9,300.0 ns |         - |
| RemoveManyFromListThenTrim        | 10000  |     282,951.4 ns |     5,650.48 ns |     9,594.95 ns |     279,200.0 ns |         - |
| RemoveManyFromDictThenTrim        | 10000  |      45,062.2 ns |       886.66 ns |     1,505.61 ns |      44,800.0 ns |         - |
| RemoveManyFromDictListThenCompact | 10000  |     125,895.7 ns |     2,495.51 ns |     3,156.02 ns |     124,900.0 ns |      40 B |
| **AppendManyToList**              | **100000** |     **100,491.1 ns** |     **2,135.46 ns** |     **5,952.80 ns** |      **98,700.0 ns** | **1048976 B** |
| AppendManyToDict                  | 100000 |   1,009,434.5 ns |    20,071.80 ns |    54,946.24 ns |     997,300.0 ns | 6037640 B |
| AppendManyToDictList              | 100000 |     196,877.9 ns |     7,929.01 ns |    22,749.83 ns |     191,700.0 ns | 2097568 B |
| IterateList                       | 100000 |      52,378.6 ns |     1,051.08 ns |     1,921.96 ns |      52,200.0 ns |         - |
| IterateDict                       | 100000 |      78,946.9 ns |     1,528.99 ns |     3,053.56 ns |      78,800.0 ns |         - |
| IterateDictList                   | 100000 |     169,825.0 ns |     3,230.46 ns |     3,720.20 ns |     169,350.0 ns |      40 B |
| ReadManyFromList                  | 100000 |      12,001.1 ns |       420.21 ns |     1,198.88 ns |      11,650.0 ns |         - |
| ReadManyFromDict                  | 100000 |      57,667.9 ns |     1,033.35 ns |     1,482.00 ns |      57,200.0 ns |         - |
| ReadManyFromDictList              | 100000 |      45,614.3 ns |     3,899.90 ns |    11,376.19 ns |      48,350.0 ns |         - |
| RemoveManyFromListThenTrim        | 100000 | 574,916,380.0 ns | 3,604,727.30 ns | 3,371,864.15 ns | 574,680,600.0 ns |         - |
| RemoveManyFromDictThenTrim        | 100000 |     335,313.0 ns |     7,262.83 ns |    21,414.62 ns |     329,000.0 ns |         - |
| RemoveManyFromDictListThenCompact | 100000 |     861,680.0 ns |    12,039.11 ns |    11,261.39 ns |     859,900.0 ns |      40 B |
