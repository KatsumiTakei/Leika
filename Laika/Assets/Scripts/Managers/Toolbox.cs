
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(WeaponManager))]
[RequireComponent(typeof(AudioManager))]
public class Toolbox : UnityDLL.SingletonMonoBehaviour<Toolbox>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        new GameObject("Manager", typeof(Toolbox));
        LuaManager.Start();
        LuaData.Start();
        EventManager.OnApplicationQuit += LuaManager.OnApplicationQuit;


    }

    void Update()
    {
        InputManager.Update();
    }

    private void OnApplicationQuit()
    {
        EventManager.BroadcastApplicationQuit();
    }
}
