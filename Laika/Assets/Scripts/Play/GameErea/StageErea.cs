using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageErea : MonoBehaviour
{
    private Renderer stageRenderer;
    private Actor []children;

    public T GetActor<T>() where T : Actor
    {
        return children.FirstOrDefault(p => p.GetComponent<T>()) as T;
    }
    public T[] GetActors<T>() where T : Actor
    {
        return children.Where(p => p.GetComponent<T>()) as T[];
    }

    void Start()
    {
        stageRenderer = GetComponent<Renderer>();
        children = transform.GetComponentsInChildren<Actor>();

        var size = new Vector2(stageRenderer.bounds.size.x, stageRenderer.bounds.size.y * 0.5f);
        Actor.SetStageRange(-size, size);
    }

    void Update()
    {
    }
}
