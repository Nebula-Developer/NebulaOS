using System;
using System.IO;
using System.Collections.Generic;

using NebulaOS.Files;
using NebulaOS.Files.NJSON;

namespace NebulaOS.NSystem {
  public class Deps {
    /// <summary>
    /// This function will create all dependencies for the NebulaOS system.
    /// This includes package created dependencies, along with system dependencies.
    /// </summary>
    public static void CreateDeps() {
      
    }

    /// <summary>
    /// Initial system creation call
    /// </summary>
    public static void CreateSystem() {
      OSFile.CreateNonExisting(Paths.GetRootPath("config.json"), JSON.Serialize(new RootConfig(), true));
      dynamic? config = JSON.ParseFile(Paths.GetRootPath("config.json"));
      if (config == null) { OSFile.Write(Paths.GetRootPath("config.json"), JSON.Serialize(new RootConfig(), true)); config = JSON.ParseFile(Paths.GetRootPath("config.json")); }

      OSFile.CreateNonExisting(Paths.GetRootPath("sysinfo.json"), JSON.Serialize(new Info(), true));
      dynamic? sysinfo = JSON.ParseFile(Paths.GetRootPath("sysinfo.json"));
      if (sysinfo == null) { OSFile.Write(Paths.GetRootPath("sysinfo.json"), JSON.Serialize(new Info(), true)); sysinfo = JSON.ParseFile(Paths.GetRootPath("sysinfo.json")); }

      List<String> undefinedRootConfigValues = JSON.GetUndefinedValues(sysinfo, typeof(RootConfig));
      List<String> undefinedInfoValues = JSON.GetUndefinedValues(sysinfo, typeof(Info));

      foreach (String val in undefinedRootConfigValues)
        Logging.LogError("RootConfig value " + val + " is undefined.");

      foreach (String val in undefinedInfoValues)
        Logging.LogError("Info value " + val + " is undefined.");

      if (undefinedRootConfigValues.Count > 0 || undefinedInfoValues.Count > 0) {
        Logging.LogError("System creation failed. Please fix the above errors and restart the system.");
        Console.WriteLine("There was an issue with the system bootup. Please fix the errors below and restart the system.");
        foreach (String val in undefinedRootConfigValues)
          Console.WriteLine("RootConfig value " + val + " is undefined.");
        foreach (String val in undefinedInfoValues)
          Console.WriteLine("Info value " + val + " is undefined.");  
        Console.WriteLine("Press any key to exit...");
      }

      Root.Config = JSON.ParseFile<RootConfig>(Paths.GetRootPath("config.json")) ?? new RootConfig();
      Root.SystemInfo = JSON.ParseFile<Info>(Paths.GetRootPath("sysinfo.json")) ?? new Info();

      Console.ReadKey();
      return;
    }
  }
}