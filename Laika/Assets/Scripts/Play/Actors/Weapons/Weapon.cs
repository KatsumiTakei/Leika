using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Actor
{
    protected float angle = 1;


    public override void OutStageRange()
    {
        WeaponManager.Instance.Return(this);
    }

    public override void Initialize()
    {
    }
}
