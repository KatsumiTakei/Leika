using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;
using System;

public enum eEnemyType
{

    Small,
}


public class Enemy : Character
{
    private float angle = 1;
    Action move;

    public override void OutStageRange()
    {
        EnemyManager.Instance.Return(this);
    }

    protected override void Destory()
    {
        //  TODO    :   implement explosion particle
        EnemyManager.Instance.Return(this);
    }

    protected override void TakeDamage()
    {
        //  TODO    :   implement hit se
        if (--Life < 0)
            Destory();
    }

    public override void Initialize()
    {
        Spd = 2;
        Life = 1;
        angle = Mathf.Deg2Rad * 270;
        transform.localPosition = Vector3.zero;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        transform.AddPosition(Mathf.Cos(angle) * Spd, Mathf.Sin(angle) * Spd, 0);
        if (!IsInStageRange())
            OutStageRange();
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        var weapon = collider2d.GetComponent<PlayerBullet>();
        if (weapon)
            TakeDamage();
    }
}
