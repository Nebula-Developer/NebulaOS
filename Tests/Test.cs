using System;
using System.Collections.Generic;
using NebulaOS.Tests.TestClasses;

namespace NebulaOS.Tests {
  public class Test {

    public void Run() {
      this.Init();
      this.End();
    }

    public virtual void End() { }
    public virtual void Init() { }

    public virtual bool CallOnBoot() { return false; }
  }

  public class GlobalTests {
    public static List<Test> Tests = new List<Test>() {
      new EaseTest()
    };
  }
}