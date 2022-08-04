using System;
using System.Collections.Generic;
using System.Linq;

using NebulaOS.Files;
using NebulaOS.Files.JSON;
using NebulaOS.System;

namespace NebulaOS {
    public class Root {
        public static int Main(String[] args) {
            dynamic? json = JSON.ParseFile(Paths.GetRootPath("NebulaOS.deps.json"));
            if (json != null) {
                Console.WriteLine(json.runtimeTarget.name);
            }
            return 0;
        }
    }
}