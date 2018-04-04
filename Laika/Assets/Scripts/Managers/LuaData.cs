

using LuaInterface;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LuaData
{
    public static List<LuaFunction> MovePattern { get; private set; } = new List<LuaFunction>();
    public static List<LuaFunction> ShotPattern { get; private set; } = new List<LuaFunction>();
    static FileInfo[] files;

    public static void Start()
    {
        DirectoryInfo di = new DirectoryInfo(LuaConst.luaDir);
        files = di.GetFiles("*.lua", SearchOption.AllDirectories);
        foreach (var lFile in files)
        {
            LuaManager.LoadFile(lFile.FullName);
        }

        double MoveMax = (double)LuaManager.Lua["MovePatternMax"];
        for (int lIndex = 0; lIndex < (int)MoveMax; lIndex++)
        {
            string methodName = "MovePattern" + lIndex;
            MovePattern.Add(LuaManager.GetFunction(methodName));
        }

        double ShotMax = (double)LuaManager.Lua["ShotPatternMax"];
        for (int lIndex = 0; lIndex < (int)ShotMax; lIndex++)
        {
            string methodName = "ShotPattern" + lIndex;
            ShotPattern.Add(LuaManager.GetFunction(methodName));
        }
    }


    public static void Reload()
    {
        foreach (var lFile in files)
        {
            LuaManager.LoadFile(lFile.FullName);
        }
    }

    public static void Reload(string fileName)
    {
        LuaManager.LoadFile(fileName);
    }
}
