using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace Linq2d.CodeGen
{
    public static class VectorData
    {
        static VectorData() => Init();
        private static IEnumerable<(int size, IVectorInfo info)> GetInfos()
        {
            yield return (2, new Vector2Info());
            yield return (4, new Vector4Info());
            yield return (8, new Vector8Info());
            yield return (16, new Vector16Info());
            yield return (32, new Vector32Info());
        }

        public static void Init()
        {
            var avis = (from i in GetInfos() where i.info.Available select i).ToList();
            MinStep = (from vi in avis select vi.size).Min();
            MaxStep = (from vi in avis select vi.size).Max();
            VectorInfo = new Dictionary<int, IVectorInfo>(from vi in avis select KeyValuePair.Create(vi.size, vi.info));
        }

        public static int MinStep { get; private set; }
        public static int MaxStep { get; private set; }
        public static IReadOnlyDictionary<int, IVectorInfo> VectorInfo { get; private set; }
        public static IEnumerable<int> StepSizesDesc { get => from sv in VectorInfo.Keys orderby sv descending select sv; }

        [ExcludeFromCodeCoverage]
        public static R Load<T, R>(T[,] array, int i, int j, int size)
            => throw new NotImplementedException();
    }
}
