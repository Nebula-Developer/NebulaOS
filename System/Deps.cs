using System;
using System.IO;

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
    public static RootConfig CreateSystem() {
      OSFile.CreateNonExisting(Paths.GetRootPath("config.json"), JSON.Serialize(new RootConfig(), true));
      dynamic? config = JSON.ParseFile(Paths.GetRootPath("config.json"));
      if (config == null) { OSFile.Write(Paths.GetRootPath("config.json"), JSON.Serialize(new RootConfig(), true)); config = JSON.ParseFile(Paths.GetRootPath("config.json")); }
      
      List<Tuple<String, String>> UnknownValues = JSON.GetUnknownValues(config, typeof(RootConfig));
      List<String> UndefinedValues = JSON.GetUndefinedValues(config, typeof(RootConfig));

      if (UndefinedValues.Count > 0) {
        foreach (String value in UndefinedValues) {
          Logging.LogError("Value " + value + " is undefined (or in wrong type) in root/config.json");
        }
      }

      if (UndefinedValues.Count > 0 || config == null) {
        Console.WriteLine("Found undefined values. Would you like to reset the root config? (y/n)");
        if (Console.ReadKey(true).Key == ConsoleKey.Y) {
          OSFile.Write(Paths.GetRootPath("config.json"), JSON.Serialize(new RootConfig(), true));
          config = JSON.ParseFile(Paths.GetRootPath("config.json"));
        } else {
          Console.WriteLine("Please fix before rebooting.\nExiting...");
          Environment.Exit(0);
        }
      }

      if (UnknownValues.Count > 0) {
        foreach (Tuple<String, String> value in UnknownValues) {
          Logging.LogError("Value " + value.Item1 + " is unknown in root/config.json, we recommend you remove it.");
        }
      }

      return JSON.ParseFile<RootConfig>(Paths.GetRootPath("config.json")) ?? new RootConfig();
    }
  }
}