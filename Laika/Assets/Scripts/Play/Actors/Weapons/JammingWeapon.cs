using System.Collections;
using System.Collections.Generic;
using UnityDLL;
using UnityEngine;

public class JammingWeapon : Weapon
{
    protected virtual void Start()
    {// HACK    :   対戦しているプレイヤーを対象にする
        Spd = 16;
        var player = GameObject.Find("StageEreaRight").transform.GetComponentInChildren<Player>();
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
    }

    protected virtual void Update()
    {

        transform.AddPosition(Mathf.Cos(angle) * Spd, Mathf.Sin(angle) * Spd, 0);


        if (!IsInStageRange())
            OutStageRange();
    }
}
