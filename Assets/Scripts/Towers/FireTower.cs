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
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,2,0.5f,5,-0.1f,1),
            new TowerUpgrade(5,3,0.5f,5,-0.1f,1),
        };
    }
    public override Debuff getDebuff()
    {
        return new FireDebuff(tickDamage,tickTime,DebuffDuration,Target);
    }
}
