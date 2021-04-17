// Decompiled with JetBrains decompiler
// Type: JezekT.WPF.Core.Properties.Resources
// Assembly: JezekT.WPF.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E7C557A7-7AEA-4D8A-895A-E714CF67A3F2
// Assembly location: C:\Users\jezek\Desktop\Client\JezekT.WPF.Core.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace JezekT.WPF.Core.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (JezekT.WPF.Core.Properties.Resources.resourceMan == null)
          JezekT.WPF.Core.Properties.Resources.resourceMan = new ResourceManager("JezekT.WPF.Core.Properties.Resources", typeof (JezekT.WPF.Core.Properties.Resources).Assembly);
        return JezekT.WPF.Core.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => JezekT.WPF.Core.Properties.Resources.resourceCulture;
      set => JezekT.WPF.Core.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap Edit => (Bitmap) JezekT.WPF.Core.Properties.Resources.ResourceManager.GetObject(nameof (Edit), JezekT.WPF.Core.Properties.Resources.resourceCulture);

    internal static Bitmap PlusLarge => (Bitmap) JezekT.WPF.Core.Properties.Resources.ResourceManager.GetObject(nameof (PlusLarge), JezekT.WPF.Core.Properties.Resources.resourceCulture);

    internal static Bitmap Trash => (Bitmap) JezekT.WPF.Core.Properties.Resources.ResourceManager.GetObject(nameof (Trash), JezekT.WPF.Core.Properties.Resources.resourceCulture);
  }
}
