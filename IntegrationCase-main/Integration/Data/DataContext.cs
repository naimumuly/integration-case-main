using Integration.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Data
{
    public static class DataContext
    {
        public static ConcurrentBag<Item> Items { get; set; } = new();
        public static int RequestCount { get; set; } = 0;
    }
}
