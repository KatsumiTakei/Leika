using UnityEngine;

public class SceneBaseMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; set; }

    protected virtual void OnEnable()
    {
        Instance = GetComponent<T>();
    }

    protected virtual void OnDisable()
    {
        Instance = null;
    }

}
