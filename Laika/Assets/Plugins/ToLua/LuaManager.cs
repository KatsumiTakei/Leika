using UnityEngine;
using LuaInterface;

public class LuaManager
{
    public static void Start()
    {
        Lua.Start();
        DelegateFactory.Init();
        Lua.AddSearchPath(Application.dataPath);
    }

    public static void OnApplicationQuit()
    {
        Lua.Dispose();
    }

    public static LuaState Lua { get; private set; } = new LuaState();

    public static T GetVariable<T>(string variableName)
    {
        return (T)Lua[variableName];
    }

    public static void Call(string methodName)
    {
        LuaFunction func = Lua.GetFunction(methodName);
        func.Call();
    }
    public static void Call<Args>(string methodName, params Args[] args)
    {
        LuaFunction func = Lua.GetFunction(methodName);
        func.Call(args);
    }

    public static Ret Invoke<Ret>(string methodName)
    {
        LuaFunction func = Lua.GetFunction(methodName);
        return func.Invoke<Ret>();
    }
    public static Ret Invoke<Ret, Args>(string methodName, Ret ret,params Args []args)
    {
        LuaFunction func = Lua.GetFunction(methodName);
        return func.Invoke<Ret, Args>(args);
    }

    public static void LoadFile(string fileName)
    {
        Lua.DoFile(fileName);
    }

    public static LuaFunction GetFunction(string methodName)
    {
        return Lua.GetFunction(methodName);
    }


}
