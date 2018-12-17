using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_mods_Beta
{
    class Program
    {
        static void Main(string[] args)
        {
            Mod.PathSkyrim = @"D:\Skyrim LE\Skyrim - Requiem for a Dream v3.6.2\Requiem for a Dream XP v3.6.2\Data";
            Mod.InitializeSkyrim();

            Mod.PathNexus = @"D:\Skyrim LE\Mods";
            Mod.InitializeNexus();

            Mod.InitializeResult();

            Mod.PathResult = @"D:\Skyrim LE\Reqiem for s Dream Result";

            Mod.Create();
        }
    }
}
