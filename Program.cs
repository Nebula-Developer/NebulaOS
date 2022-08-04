using System;
using System.Collections.Generic;
using System.Linq;

using NebulaOS.Files;

namespace NebulaOS {
    public class Root {
        public static int Main(String[] args) {
            Console.WriteLine(File.Exists(Paths.GetRootPath("hello.txt")));
            return 0;
        }
    }
}