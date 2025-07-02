```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4484/24H2/2024Update/HudsonValley)
13th Gen Intel Core i7-13700HX 2.10GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 9.0.300
  [Host]     : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2
  Job-CNUJVU : .NET 9.0.5 (9.0.525.21509), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method                           | N      | Mean             | Error           | StdDev          | Median           | Allocated |
|--------------------------------- |------- |-----------------:|----------------:|----------------:|-----------------:|----------:|
| **AppendManyToList**                 | **10**     |       **1,191.0 ns** |        **97.98 ns** |       **288.88 ns** |       **1,150.0 ns** |     **216 B** |
| AppendManyToDict                 | 10     |       1,528.0 ns |       109.30 ns |       322.26 ns |       1,500.0 ns |     776 B |
| AppendManyToSortedDict           | 10     |       3,852.6 ns |       114.00 ns |       330.75 ns |       3,900.0 ns |     592 B |
| AppendManyToDictList             | 10     |       1,327.6 ns |        83.32 ns |       243.06 ns |       1,350.0 ns |     360 B |
| IterateList                      | 10     |         258.2 ns |        28.24 ns |        82.39 ns |         300.0 ns |         - |
| IterateDict                      | 10     |         339.4 ns |        24.27 ns |        71.17 ns |         300.0 ns |         - |
| IterateSortedDict                | 10     |       2,579.8 ns |       104.15 ns |       297.15 ns |       2,600.0 ns |     104 B |
| IterateDictList                  | 10     |       1,729.3 ns |        93.47 ns |       274.14 ns |       1,700.0 ns |      48 B |
| ReadManyFromList                 | 10     |         124.5 ns |        20.38 ns |        60.09 ns |         150.0 ns |         - |
| ReadManyFromDict                 | 10     |         345.4 ns |        26.34 ns |        76.40 ns |         300.0 ns |         - |
| ReadManyFromSortedDict           | 10     |         689.8 ns |        48.85 ns |       142.50 ns |         700.0 ns |         - |
| ReadManyFromDictList             | 10     |         256.7 ns |        26.28 ns |        76.25 ns |         300.0 ns |         - |
| RemoveManyFromListInPlace        | 10     |         546.5 ns |        47.80 ns |       140.20 ns |         500.0 ns |         - |
| RemoveManyFromListWithLinq       | 10     |       2,554.4 ns |       121.58 ns |       338.92 ns |       2,500.0 ns |     136 B |
| RemoveManyFromDictInPlace        | 10     |         533.7 ns |        41.18 ns |       118.15 ns |         600.0 ns |         - |
| RemoveManyFromDictWithLinq       | 10     |       3,130.6 ns |       147.22 ns |       429.43 ns |       3,100.0 ns |     296 B |
| RemoveManyFromSortedDictInPlace  | 10     |       2,497.0 ns |       126.50 ns |       371.00 ns |       2,500.0 ns |     104 B |
| RemoveManyFromSortedDictWithLinq | 10     |       4,138.8 ns |       178.07 ns |       519.44 ns |       4,200.0 ns |     504 B |
| RemoveManyFromDictListInPlace    | 10     |       1,979.3 ns |        90.77 ns |       266.21 ns |       1,950.0 ns |      48 B |
| RemoveManyFromDictListWithLinq   | 10     |       3,583.3 ns |       147.34 ns |       432.13 ns |       3,550.0 ns |     360 B |
| **AppendManyToList**                 | **100**    |       **2,100.0 ns** |        **86.50 ns** |       **255.05 ns** |       **2,100.0 ns** |    **1184 B** |
| AppendManyToDict                 | 100    |       2,898.0 ns |       114.43 ns |       337.39 ns |       2,950.0 ns |    7392 B |
| AppendManyToSortedDict           | 100    |      41,704.1 ns |       828.80 ns |     2,064.01 ns |      41,100.0 ns |    4912 B |
| AppendManyToDictList             | 100    |       2,514.3 ns |        88.74 ns |       258.86 ns |       2,500.0 ns |    2224 B |
| IterateList                      | 100    |       1,028.7 ns |        36.06 ns |        98.72 ns |       1,000.0 ns |         - |
| IterateDict                      | 100    |       1,093.0 ns |        48.98 ns |       144.43 ns |       1,100.0 ns |         - |
| IterateSortedDict                | 100    |       6,059.8 ns |       223.75 ns |       649.14 ns |       5,900.0 ns |     152 B |
| IterateDictList                  | 100    |       4,687.6 ns |       132.31 ns |       383.86 ns |       4,600.0 ns |      48 B |
| ReadManyFromList                 | 100    |         173.7 ns |        17.42 ns |        44.33 ns |         200.0 ns |         - |
| ReadManyFromDict                 | 100    |         487.9 ns |        42.82 ns |       125.58 ns |         500.0 ns |         - |
| ReadManyFromSortedDict           | 100    |       3,224.6 ns |        68.08 ns |       159.13 ns |       3,200.0 ns |         - |
| ReadManyFromDictList             | 100    |         351.5 ns |        29.37 ns |        86.15 ns |         400.0 ns |         - |
| RemoveManyFromListInPlace        | 100    |       2,586.5 ns |       101.53 ns |       292.94 ns |       2,600.0 ns |         - |
| RemoveManyFromListWithLinq       | 100    |       4,805.1 ns |       209.87 ns |       615.51 ns |       4,900.0 ns |     288 B |
| RemoveManyFromDictInPlace        | 100    |       1,989.4 ns |        43.47 ns |       102.48 ns |       2,000.0 ns |         - |
| RemoveManyFromDictWithLinq       | 100    |       6,540.0 ns |       275.30 ns |       789.88 ns |       6,500.0 ns |     880 B |
| RemoveManyFromSortedDictInPlace  | 100    |       7,086.6 ns |       251.90 ns |       730.81 ns |       7,000.0 ns |     152 B |
| RemoveManyFromSortedDictWithLinq | 100    |      10,579.6 ns |       237.47 ns |       673.66 ns |      10,600.0 ns |    1808 B |
| RemoveManyFromDictListInPlace    | 100    |       5,840.0 ns |       118.30 ns |       252.10 ns |       5,900.0 ns |      48 B |
| RemoveManyFromDictListWithLinq   | 100    |       9,144.4 ns |       185.98 ns |       518.45 ns |       9,200.0 ns |     752 B |
| **AppendManyToList**                 | **1000**   |       **8,205.2 ns** |       **653.40 ns** |     **1,885.20 ns** |       **7,750.0 ns** |    **8424 B** |
| AppendManyToDict                 | 1000   |      14,315.2 ns |       514.83 ns |     1,452.09 ns |      14,200.0 ns |   73168 B |
| AppendManyToSortedDict           | 1000   |     554,975.0 ns |    10,964.72 ns |    12,626.99 ns |     553,050.0 ns |   48112 B |
| AppendManyToDictList             | 1000   |      13,792.3 ns |       426.46 ns |     1,195.85 ns |      13,500.0 ns |   16632 B |
| IterateList                      | 1000   |       6,713.8 ns |       510.35 ns |     1,456.07 ns |       6,150.0 ns |         - |
| IterateDict                      | 1000   |       8,518.7 ns |       249.46 ns |       699.51 ns |       8,500.0 ns |         - |
| IterateSortedDict                | 1000   |      46,429.8 ns |     1,416.68 ns |     4,041.87 ns |      45,800.0 ns |     200 B |
| IterateDictList                  | 1000   |      38,305.2 ns |     1,030.58 ns |     2,989.89 ns |      37,700.0 ns |      48 B |
| ReadManyFromList                 | 1000   |       1,750.0 ns |        36.48 ns |        60.94 ns |       1,700.0 ns |         - |
| ReadManyFromDict                 | 1000   |       2,220.0 ns |       130.15 ns |       383.76 ns |       2,100.0 ns |         - |
| ReadManyFromSortedDict           | 1000   |      40,556.5 ns |       803.65 ns |     1,830.31 ns |      40,600.0 ns |         - |
| ReadManyFromDictList             | 1000   |       2,138.0 ns |       119.47 ns |       352.27 ns |       1,950.0 ns |         - |
| RemoveManyFromListInPlace        | 1000   |      22,047.4 ns |       919.78 ns |     2,639.02 ns |      21,300.0 ns |         - |
| RemoveManyFromListWithLinq       | 1000   |      20,605.4 ns |       506.89 ns |     1,429.70 ns |      20,300.0 ns |    1856 B |
| RemoveManyFromDictInPlace        | 1000   |      16,379.8 ns |       330.97 ns |       889.13 ns |      16,200.0 ns |         - |
| RemoveManyFromDictWithLinq       | 1000   |      39,016.9 ns |       773.47 ns |     1,713.95 ns |      38,700.0 ns |    7496 B |
| RemoveManyFromSortedDictInPlace  | 1000   |      53,330.9 ns |     1,070.10 ns |     2,819.07 ns |      53,400.0 ns |     200 B |
| RemoveManyFromSortedDictWithLinq | 1000   |      85,048.3 ns |     1,698.30 ns |     2,489.35 ns |      84,600.0 ns |   15624 B |
| RemoveManyFromDictListInPlace    | 1000   |      45,750.8 ns |       886.37 ns |     2,036.59 ns |      46,000.0 ns |      48 B |
| RemoveManyFromDictListWithLinq   | 1000   |      54,439.5 ns |     1,035.51 ns |     2,635.71 ns |      54,100.0 ns |    5016 B |
| **AppendManyToList**                 | **10000**  |      **18,120.2 ns** |       **877.60 ns** |     **2,503.83 ns** |      **17,000.0 ns** |  **131400 B** |
| AppendManyToDict                 | 10000  |      91,539.0 ns |     3,473.61 ns |    10,242.02 ns |      92,050.0 ns |  673064 B |
| AppendManyToSortedDict           | 10000  |   4,620,126.7 ns |    65,122.47 ns |    60,915.60 ns |   4,615,200.0 ns |  480112 B |
| AppendManyToDictList             | 10000  |      32,894.8 ns |     1,121.16 ns |     3,252.70 ns |      32,000.0 ns |  262488 B |
| IterateList                      | 10000  |      10,376.3 ns |       344.58 ns |       977.51 ns |      10,100.0 ns |         - |
| IterateDict                      | 10000  |      15,671.9 ns |       574.81 ns |     1,658.45 ns |      15,200.0 ns |         - |
| IterateSortedDict                | 10000  |     352,507.3 ns |     7,031.61 ns |    12,679.44 ns |     349,600.0 ns |     264 B |
| IterateDictList                  | 10000  |      49,128.6 ns |     1,207.22 ns |     3,521.51 ns |      48,400.0 ns |      48 B |
| ReadManyFromList                 | 10000  |       6,650.6 ns |       358.17 ns |       992.49 ns |       6,500.0 ns |         - |
| ReadManyFromDict                 | 10000  |      12,428.0 ns |       722.28 ns |     2,048.99 ns |      12,100.0 ns |         - |
| ReadManyFromSortedDict           | 10000  |     292,766.7 ns |     5,780.60 ns |     6,185.18 ns |     291,750.0 ns |         - |
| ReadManyFromDictList             | 10000  |      10,310.3 ns |       804.58 ns |     2,334.23 ns |       9,800.0 ns |         - |
| RemoveManyFromListInPlace        | 10000  |     267,735.0 ns |     5,108.00 ns |     5,882.38 ns |     267,500.0 ns |         - |
| RemoveManyFromListWithLinq       | 10000  |      31,423.2 ns |       980.13 ns |     2,812.19 ns |      31,000.0 ns |   15096 B |
| RemoveManyFromDictInPlace        | 10000  |      44,316.7 ns |       875.51 ns |     2,164.05 ns |      43,950.0 ns |         - |
| RemoveManyFromDictWithLinq       | 10000  |     340,111.1 ns |     6,666.77 ns |    11,138.68 ns |     340,700.0 ns |   73272 B |
| RemoveManyFromSortedDictInPlace  | 10000  |     391,010.3 ns |     7,708.61 ns |    13,501.01 ns |     391,600.0 ns |     264 B |
| RemoveManyFromSortedDictWithLinq | 10000  |     940,602.4 ns |    18,606.40 ns |    33,551.18 ns |     934,700.0 ns |  153400 B |
| RemoveManyFromDictListInPlace    | 10000  |     121,511.1 ns |     2,433.56 ns |     4,065.94 ns |     121,200.0 ns |      48 B |
| RemoveManyFromDictListWithLinq   | 10000  |     122,664.3 ns |     2,404.66 ns |     3,448.69 ns |     121,750.0 ns |   43424 B |
| **AppendManyToList**                 | **100000** |     **154,905.3 ns** |     **5,421.61 ns** |    **15,555.61 ns** |     **151,700.0 ns** | **1048976 B** |
| AppendManyToDict                 | 100000 |   1,072,687.1 ns |    28,668.82 ns |    81,328.68 ns |   1,059,400.0 ns | 6037640 B |
| AppendManyToSortedDict           | 100000 |  10,507,415.0 ns |   207,859.85 ns |   239,371.72 ns |  10,521,000.0 ns | 4800112 B |
| AppendManyToDictList             | 100000 |     316,962.1 ns |    13,452.12 ns |    38,596.67 ns |     313,900.0 ns | 2097536 B |
| IterateList                      | 100000 |      49,721.1 ns |     4,498.78 ns |    12,907.84 ns |      52,700.0 ns |         - |
| IterateDict                      | 100000 |     161,555.3 ns |     9,577.97 ns |    27,326.48 ns |     156,850.0 ns |         - |
| IterateSortedDict                | 100000 |     587,328.1 ns |    30,300.54 ns |    83,962.66 ns |     557,800.0 ns |     312 B |
| IterateDictList                  | 100000 |     176,027.1 ns |     6,277.63 ns |    18,112.41 ns |     171,450.0 ns |      48 B |
| ReadManyFromList                 | 100000 |      16,823.2 ns |       693.72 ns |     1,990.43 ns |      16,300.0 ns |         - |
| ReadManyFromDict                 | 100000 |     147,423.9 ns |    12,008.37 ns |    33,074.54 ns |     143,650.0 ns |         - |
| ReadManyFromSortedDict           | 100000 |   1,321,873.4 ns |    58,580.38 ns |   167,133.14 ns |   1,290,100.0 ns |         - |
| ReadManyFromDictList             | 100000 |      65,483.0 ns |     3,301.94 ns |     9,420.61 ns |      64,100.0 ns |         - |
| RemoveManyFromListInPlace        | 100000 | 561,012,878.6 ns | 4,363,954.69 ns | 3,868,532.17 ns | 560,050,350.0 ns |         - |
| RemoveManyFromListWithLinq       | 100000 |     103,955.9 ns |     3,429.24 ns |     9,728.18 ns |     103,700.0 ns |  198072 B |
| RemoveManyFromDictInPlace        | 100000 |     316,069.9 ns |     9,977.32 ns |    28,304.01 ns |     312,900.0 ns |         - |
| RemoveManyFromDictWithLinq       | 100000 |     884,912.8 ns |    24,785.73 ns |    67,431.40 ns |     888,100.0 ns |  673168 B |
| RemoveManyFromSortedDictInPlace  | 100000 |     809,594.6 ns |    26,942.37 ns |    75,991.45 ns |     795,350.0 ns |     312 B |
| RemoveManyFromSortedDictWithLinq | 100000 |   1,693,104.0 ns |    33,328.28 ns |    67,324.78 ns |   1,684,500.0 ns | 1473296 B |
| RemoveManyFromDictListInPlace    | 100000 |     789,253.8 ns |    15,679.89 ns |    27,462.07 ns |     785,100.0 ns |      48 B |
| RemoveManyFromDictListWithLinq   | 100000 |     793,366.1 ns |    15,835.55 ns |    35,090.48 ns |     787,300.0 ns |  529280 B |
