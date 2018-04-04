using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leika : Player
{

    protected override void Start()
    {
        base.Start();
        base.Initialize(nameof(Leika));
    }

    protected override void Update()
    {
        base.Update();

    }
}
