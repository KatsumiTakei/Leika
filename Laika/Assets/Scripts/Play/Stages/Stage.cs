using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using STLExtensiton;

public class Stage : MonoBehaviour
{
    void Start()
    {// HACK    :   MasterDataから拾う
        var enemies = Resources.LoadAll<Enemy>(ResourcesPath.Prefabs.Enemies.Path);
        Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(p => { EnemyManager.Instance.Create(enemies[0], gameObject); });
    }

    void Update()
    {
    }


    public void ClearLivingEnemies()
    {
        var enemies = GetComponentsInChildren<Enemy>();
        enemies.ForEach(p => { EnemyManager.Instance.Return(p); });
    }


}
