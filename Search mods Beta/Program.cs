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
            #region На рабое
            //Mod.PathSkyrim = @"D:\Skyrim LE\Skyrim - Requiem for a Dream v3.6.2\Requiem for a Dream XP v3.6.2\Data";
            //Mod.PathNexus = @"D:\Skyrim LE\Mods";
            //Mod.PathResult = @"D:\Skyrim LE\Reqiem for s Dream Result";
            #endregion

            #region Дома

            Mod.PathSkyrim = @"E:\Games\Skyrim - Requiem for a Dream v4.0\Requiem for a Dream XP v4.0\Data";
            Mod.PathNexus = @"E:\MySkyrimLE\Source Requiem for a Dream";
            Mod.PathResult = @"E:\MySkyrimLE\Sourse for MO Xaxdr";

            #endregion


            Mod.InitializeSkyrim();


            Mod.InitializeNexus();

            Mod.InitializeResult();

           // Mod.Display(true);

           Mod.Create(" (Xandr)");
        }
    }
}
