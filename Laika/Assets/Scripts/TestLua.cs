using UnityEngine;
using System.Collections;
using LuaInterface;
using System;
using System.IO;

public class TestLua : MonoBehaviour
{
    private string strLog = "";

    void Start()
    {
        Application.logMessageReceived += Log;

    }

    void Log(string msg, string stackTrace, LogType type)
    {
        strLog += msg;
        strLog += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, Screen.height / 2 - 100, 600, 400), strLog);

        if (GUI.Button(new Rect(50, 50, 120, 45), "DoFile"))
        {
            strLog = "";
            LuaManager.LoadFile(LuaConst.luaDir + "/Test.lua");
        }
        else if (GUI.Button(new Rect(50, 250, 120, 45), "Call"))
        {
            strLog = "";
            LuaFunction luaFunc = LuaManager.Lua["test.luaFunc"] as LuaFunction;

            //luaFunc = lua.GetFunction("Test");
            //luaFunc.Call();
            int num = luaFunc.Invoke<int, int>(123456);

            //int num = lua.Invoke<int, int>("test.luaFunc", 123456, true);
            //num = (int)LuaManager.Instance.Lua["PlayScreen"];
            Debugger.Log("generic call return: {0}", num);
        }

        //lua.Collect();
        //lua.CheckTop();
    }

    void OnApplicationQuit()
    {
        Application.logMessageReceived -= Log;
    }
}
