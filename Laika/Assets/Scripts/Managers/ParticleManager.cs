using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ParticleManager : GenericPoolManager<ParticleManager, ParticleSystem>
{
    protected override string PrefabPath { get; set; } = ResourcesPath.Prefabs.Path;

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    public void Play(ParticleSystem type, Vector3 position, Vector3? rotation = null)
    {
        var particleSystem = Rent(type);
        particleSystem.transform.position = position;
        if(rotation != null)
            particleSystem.transform.rotation = Quaternion.Euler(rotation.Value);

        particleSystem.Play();
        Observable.Timer(TimeSpan.FromSeconds(particleSystem.main.duration)).Subscribe(p => { Return(particleSystem); });
    }
}
