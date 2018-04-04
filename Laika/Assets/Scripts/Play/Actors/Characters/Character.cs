using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Actor
{
    protected int Life { get; set; }
    protected int Count { get; set; }

    protected abstract void Destory();
    protected abstract void TakeDamage();

}
