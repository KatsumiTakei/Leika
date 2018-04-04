using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletWeapon {

    protected override void Start ()
	{
	    base.Start();
	    Spd = 16;
        angle = Mathf.Deg2Rad * 90;
    }
	
	protected override void Update () {
		base.Update();
	}
}
