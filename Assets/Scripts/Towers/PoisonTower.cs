using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    [SerializeField]
    private float tickTime;
    [SerializeField]
    private PoisonSplash splashPrefab;
    [SerializeField]
    private int splashDamage;

    public int SplashDamage { get => splashDamage;}
    public float TickTime { get => tickTime;}

    private void Start()
    {
        ElementType = Element.POISON;
    }

    public override Debuff getDebuff()
    {
        return new PoisonDebuff(splashDamage, TickTime,splashPrefab,DebuffDuration,Target) ;
    }

}

