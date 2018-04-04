using UniRx.Toolkit;
using UnityEngine;

public class GenericPool<T> : ObjectPool<T> where T : Component
{
    public T Prefab { get; set; }

    public GenericPool(T argPrefab)
    {
        Prefab = argPrefab;
    }

    protected override T CreateInstance()
    {
        var obj = GameObject.Instantiate(Prefab);
        obj.name = obj.name.Replace("(Clone)","");
        return obj;
    }
}
