```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4351/24H2/2024Update/HudsonValley)
13th Gen Intel Core i7-13700HX 2.10GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 9.0.300
  [Host]     : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2
  Job-CNUJVU : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method                         | N      | Mean             | Error           | StdDev          | Median           | Allocated |
|------------------------------- |------- |-----------------:|----------------:|----------------:|-----------------:|----------:|
| **AppendManyToList**               | **10**     |       **1,210.4 ns** |       **103.61 ns** |       **298.94 ns** |       **1,150.0 ns** |     **216 B** |
| AppendManyToDict               | 10     |       1,407.1 ns |       131.45 ns |       385.52 ns |       1,300.0 ns |     776 B |
| AppendManyToDictList           | 10     |       1,339.0 ns |       101.22 ns |       298.45 ns |       1,300.0 ns |     360 B |
| IterateList                    | 10     |         281.2 ns |        30.72 ns |        88.63 ns |         300.0 ns |         - |
| IterateDict                    | 10     |         330.9 ns |        32.00 ns |        92.83 ns |         300.0 ns |         - |
| IterateDictList                | 10     |       1,755.6 ns |       107.52 ns |       315.33 ns |       1,700.0 ns |      40 B |
| ReadManyFromList               | 10     |         171.4 ns |        22.56 ns |        65.80 ns |         200.0 ns |         - |
| ReadManyFromDict               | 10     |         297.0 ns |        33.91 ns |        99.44 ns |         300.0 ns |         - |
| ReadManyFromDictList           | 10     |         141.4 ns |        25.78 ns |        75.61 ns |         100.0 ns |         - |
| RemoveManyFromListInPlace      | 10     |         425.0 ns |        48.77 ns |       143.81 ns |         400.0 ns |         - |
| RemoveManyFromListWithLinq     | 10     |       2,357.6 ns |       144.75 ns |       424.53 ns |       2,300.0 ns |     136 B |
| RemoveManyFromDictInPlace      | 10     |         505.1 ns |        42.45 ns |       123.83 ns |         500.0 ns |         - |
| RemoveManyFromDictWithLinq     | 10     |       2,771.4 ns |       145.19 ns |       423.53 ns |       2,700.0 ns |     296 B |
| RemoveManyFromDictListInPlace  | 10     |       1,889.4 ns |       101.69 ns |       298.23 ns |       1,950.0 ns |      40 B |
| RemoveManyFromDictListWithLinq | 10     |       3,453.5 ns |       160.99 ns |       472.15 ns |       3,400.0 ns |     344 B |
| **AppendManyToList**               | **100**    |       **1,901.0 ns** |        **95.14 ns** |       **280.51 ns** |       **1,900.0 ns** |    **1184 B** |
| AppendManyToDict               | 100    |       2,568.7 ns |       127.08 ns |       372.70 ns |       2,500.0 ns |    7392 B |
| AppendManyToDictList           | 100    |       2,592.3 ns |        93.98 ns |       272.65 ns |       2,550.0 ns |    2224 B |
| IterateList                    | 100    |       1,023.7 ns |        50.76 ns |       148.86 ns |         950.0 ns |         - |
| IterateDict                    | 100    |       1,095.9 ns |        53.00 ns |       154.60 ns |       1,100.0 ns |         - |
| IterateDictList                | 100    |       4,984.7 ns |       149.90 ns |       437.27 ns |       4,900.0 ns |      40 B |
| ReadManyFromList               | 100    |         224.0 ns |        21.62 ns |        63.75 ns |         200.0 ns |         - |
| ReadManyFromDict               | 100    |         443.3 ns |        33.34 ns |        96.72 ns |         400.0 ns |         - |
| ReadManyFromDictList           | 100    |         296.0 ns |        29.29 ns |        86.36 ns |         300.0 ns |         - |
| RemoveManyFromListInPlace      | 100    |       2,372.4 ns |       114.05 ns |       332.68 ns |       2,300.0 ns |         - |
| RemoveManyFromListWithLinq     | 100    |       4,654.3 ns |       190.02 ns |       542.13 ns |       4,700.0 ns |     288 B |
| RemoveManyFromDictInPlace      | 100    |       1,954.9 ns |        41.97 ns |       110.57 ns |       1,950.0 ns |         - |
| RemoveManyFromDictWithLinq     | 100    |       6,497.9 ns |       157.25 ns |       456.20 ns |       6,500.0 ns |     880 B |
| RemoveManyFromDictListInPlace  | 100    |       5,756.7 ns |       137.24 ns |       398.15 ns |       5,800.0 ns |      40 B |
| RemoveManyFromDictListWithLinq | 100    |       9,014.6 ns |       250.89 ns |       723.88 ns |       9,000.0 ns |     736 B |
| **AppendManyToList**               | **1000**   |       **8,374.2 ns** |       **425.20 ns** |     **1,206.23 ns** |       **8,200.0 ns** |    **8424 B** |
| AppendManyToDict               | 1000   |      14,913.0 ns |       450.93 ns |     1,271.86 ns |      14,800.0 ns |   73168 B |
| AppendManyToDictList           | 1000   |      13,748.9 ns |       326.20 ns |       920.04 ns |      13,600.0 ns |   16632 B |
| IterateList                    | 1000   |       6,719.4 ns |       431.77 ns |     1,224.86 ns |       6,300.0 ns |         - |
| IterateDict                    | 1000   |       8,768.4 ns |       170.35 ns |       442.76 ns |       8,700.0 ns |         - |
| IterateDictList                | 1000   |      39,667.5 ns |       772.05 ns |     2,020.33 ns |      39,200.0 ns |      40 B |
| ReadManyFromList               | 1000   |       1,864.6 ns |        94.48 ns |       272.60 ns |       1,800.0 ns |         - |
| ReadManyFromDict               | 1000   |       2,300.0 ns |       142.71 ns |       420.80 ns |       2,150.0 ns |         - |
| ReadManyFromDictList           | 1000   |       2,182.8 ns |       135.96 ns |       398.73 ns |       2,000.0 ns |         - |
| RemoveManyFromListInPlace      | 1000   |      23,173.5 ns |       967.38 ns |     2,821.88 ns |      22,250.0 ns |         - |
| RemoveManyFromListWithLinq     | 1000   |      20,486.8 ns |       512.90 ns |     1,438.22 ns |      20,300.0 ns |    1856 B |
| RemoveManyFromDictInPlace      | 1000   |      16,509.9 ns |       331.27 ns |       928.92 ns |      16,500.0 ns |         - |
| RemoveManyFromDictWithLinq     | 1000   |      39,807.5 ns |       786.06 ns |     1,640.81 ns |      39,700.0 ns |    7496 B |
| RemoveManyFromDictListInPlace  | 1000   |      47,135.5 ns |       916.26 ns |     2,086.79 ns |      47,050.0 ns |      40 B |
| RemoveManyFromDictListWithLinq | 1000   |      54,603.1 ns |     1,085.98 ns |     2,516.92 ns |      54,700.0 ns |    5000 B |
| **AppendManyToList**               | **10000**  |      **16,833.0 ns** |       **376.46 ns** |     **1,074.07 ns** |      **16,550.0 ns** |  **131400 B** |
| AppendManyToDict               | 10000  |      92,598.0 ns |     3,734.90 ns |    11,012.44 ns |      91,750.0 ns |  673064 B |
| AppendManyToDictList           | 10000  |      29,812.5 ns |       655.00 ns |     1,889.82 ns |      29,500.0 ns |  262488 B |
| IterateList                    | 10000  |      10,617.6 ns |       320.94 ns |       899.95 ns |      10,500.0 ns |         - |
| IterateDict                    | 10000  |      15,472.6 ns |       310.01 ns |       706.04 ns |      15,250.0 ns |         - |
| IterateDictList                | 10000  |      51,868.2 ns |     1,037.25 ns |     2,444.91 ns |      51,500.0 ns |      40 B |
| ReadManyFromList               | 10000  |       6,605.6 ns |       500.62 ns |     1,395.53 ns |       6,200.0 ns |         - |
| ReadManyFromDict               | 10000  |      11,540.4 ns |       434.45 ns |     1,203.85 ns |      11,400.0 ns |         - |
| ReadManyFromDictList           | 10000  |      10,373.3 ns |       381.58 ns |     1,063.68 ns |      10,200.0 ns |         - |
| RemoveManyFromListInPlace      | 10000  |     275,718.2 ns |     5,423.13 ns |     6,660.09 ns |     272,500.0 ns |         - |
| RemoveManyFromListWithLinq     | 10000  |      29,172.8 ns |       583.67 ns |     1,537.61 ns |      28,800.0 ns |   15096 B |
| RemoveManyFromDictInPlace      | 10000  |      45,407.7 ns |       899.38 ns |     1,575.20 ns |      45,200.0 ns |         - |
| RemoveManyFromDictWithLinq     | 10000  |     354,576.0 ns |     6,989.57 ns |     9,330.88 ns |     356,400.0 ns |   73272 B |
| RemoveManyFromDictListInPlace  | 10000  |     124,525.0 ns |     2,440.66 ns |     2,810.67 ns |     123,500.0 ns |      40 B |
| RemoveManyFromDictListWithLinq | 10000  |     128,770.8 ns |     2,326.11 ns |     3,024.61 ns |     128,200.0 ns |   43408 B |
| **AppendManyToList**               | **100000** |     **112,200.0 ns** |     **4,166.58 ns** |    **12,087.99 ns** |     **108,600.0 ns** | **1048976 B** |
| AppendManyToDict               | 100000 |     868,447.6 ns |    17,269.33 ns |    31,577.94 ns |     864,850.0 ns | 6037640 B |
| AppendManyToDictList           | 100000 |     177,521.8 ns |     3,215.95 ns |     6,853.46 ns |     175,900.0 ns | 2097568 B |
| IterateList                    | 100000 |      52,181.0 ns |       983.96 ns |     1,799.22 ns |      51,400.0 ns |         - |
| IterateDict                    | 100000 |      77,596.3 ns |     1,536.51 ns |     2,153.97 ns |      77,600.0 ns |         - |
| IterateDictList                | 100000 |     168,614.3 ns |     3,298.54 ns |     2,924.07 ns |     167,600.0 ns |      40 B |
| ReadManyFromList               | 100000 |      12,135.9 ns |       345.33 ns |       974.01 ns |      11,900.0 ns |         - |
| ReadManyFromDict               | 100000 |      57,442.9 ns |     1,132.88 ns |     2,071.54 ns |      56,900.0 ns |         - |
| ReadManyFromDictList           | 100000 |      39,470.4 ns |     4,163.37 ns |    12,144.75 ns |      43,150.0 ns |         - |
| RemoveManyFromListInPlace      | 100000 | 585,542,042.9 ns | 5,326,221.37 ns | 4,721,556.51 ns | 584,393,050.0 ns |         - |
| RemoveManyFromListWithLinq     | 100000 |     101,692.3 ns |     1,914.48 ns |     1,598.68 ns |     101,800.0 ns |  198072 B |
| RemoveManyFromDictInPlace      | 100000 |     305,983.3 ns |     5,674.97 ns |     4,430.64 ns |     304,700.0 ns |         - |
| RemoveManyFromDictWithLinq     | 100000 |   3,458,000.0 ns |    63,451.08 ns |    62,317.45 ns |   3,449,050.0 ns |  673168 B |
| RemoveManyFromDictListInPlace  | 100000 |     872,503.8 ns |     6,718.62 ns |     5,610.35 ns |     872,150.0 ns |      40 B |
| RemoveManyFromDictListWithLinq | 100000 |     820,320.0 ns |    11,397.78 ns |    10,661.49 ns |     819,200.0 ns |  529264 B |
