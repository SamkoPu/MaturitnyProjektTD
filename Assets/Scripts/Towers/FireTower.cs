using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    [SerializeField]
    private float tickTime;
    [SerializeField]
    private float tickDamage;

    public float TickTime { get => tickTime; }
    public float TickDamage { get => tickDamage; }
    private void Start()
    {
        ElementType = Element.FIRE;
    }
    public override Debuff getDebuff()
    {
        return new FireDebuff(tickDamage,tickTime,DebuffDuration,Target);
    }
}
