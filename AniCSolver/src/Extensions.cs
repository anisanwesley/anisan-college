using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AniCSolver.Core
{
    public static class Extensions
    {
        //List control Extension, pego daqui: https://github.com/AnisanWesley/anisan-library/blob/master/Extensions/ListControlExtensions.cs
        [DebuggerStepThrough]
        public static bool IsEmpty(this Dictionary<string, Valor> list)
        {
            return list.Count == 0;

        }
    }
}
