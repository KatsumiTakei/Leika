using System;
using UnityEngine;
using UnityDLL;

public class BulletWeapon : Weapon
{
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        transform.AddPosition(Mathf.Cos(angle) * Spd, Mathf.Sin(angle) * Spd, 0);
        if (!IsInStageRange())
            OutStageRange();
    }

}
