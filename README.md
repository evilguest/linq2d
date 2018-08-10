``` ini

BenchmarkDotNet=v0.11.0, OS=Windows 10.0.17134.191 (1803/April2018Update/Redstone4)
Intel Core i7-6600U CPU 2.60GHz (Max: 2.61GHz) (Skylake), 1 CPU, 4 logical and 2 physical cores
Frequency=2742189 Hz, Resolution=364.6722 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3160.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3160.0


```
| Method |    W |    H | Method |       FilterType |      Mean |     Error |    StdDev |    Median |
|------- |----- |----- |------- |----------------- |----------:|----------:|----------:|----------:|
| Filter | 1000 | 1000 |     C4 |   BestLinqFilter |  2.662 ms | 0.0521 ms | 0.0621 ms |  2.674 ms |
| Filter | 1000 | 1000 |     C8 |   BestLinqFilter |  3.196 ms | 0.0629 ms | 0.1271 ms |  3.206 ms |
| Filter | 1000 | 1000 |     C4 |     UnsafeFilter |  3.848 ms | 0.0936 ms | 0.2716 ms |  3.870 ms |
| Filter | 1000 | 1000 |     C8 |     UnsafeFilter |  4.537 ms | 0.1475 ms | 0.4303 ms |  4.443 ms |
| Filter | 1000 | 1000 |     C4 | BasicArrayFilter | 11.304 ms | 0.3246 ms | 0.9416 ms | 11.180 ms |
| Filter | 1000 | 1000 |     C8 | BasicArrayFilter | 17.157 ms | 0.9320 ms | 2.7480 ms | 16.348 ms |
