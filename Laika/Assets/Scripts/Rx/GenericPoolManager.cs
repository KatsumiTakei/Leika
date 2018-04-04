using System.Collections.Generic;
using UnityEngine;
using STLExtensiton;
using UniRx;

public abstract class GenericPoolManager<T, Value> : MonoBehaviour where T : MonoBehaviour where Value : Component
{

    #region Singleton

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError(typeof(T) + " is nothing.");
                }
            }
            return _instance;
        }

    }

    protected void Awake()
    {
        if (this != Instance)
        {
            Debug.Log(name + " generated.");
            Destroy(this);
            return;
        }

        DontDestroyThisOnLoad();
    }

    public void DontDestroyThisOnLoad()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    readonly Dictionary<string, GenericPool<Value>> values = new Dictionary<string, GenericPool<Value>>();
    protected abstract string PrefabPath { get; set; }

    protected virtual void OnEnable()
    {// ResourcesLoadがUnityのライフサイクルからしかコールできない

        var prefabs = Resources.LoadAll<Value>(PrefabPath);
        prefabs.ForEach(p => { values.AddIfNotExists(p.name, new GenericPool<Value>(p)); });
    }

    public virtual Value Rent(Value target)
    {
        return values[target.name].Rent();
    }

    public virtual void Return(Value target)
    {
        values[target.name].Return(target);
    }

    public virtual void PreloadAsync(Value target, int preloadCount, int threshold)
    {
        values[target.name].PreloadAsync(preloadCount, threshold).Subscribe();
    }

    public virtual void Shrink(Value target, int instanceCountRatio, int minSize)
    {
        values[target.name].Shrink(instanceCountRatio, minSize);
    }
}
