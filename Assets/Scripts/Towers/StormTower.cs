using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    private void Start()
    {
        ElementType = Element.STORM;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,2,1,2),
            new TowerUpgrade(5,3,1,2),
        };
    }
    public override Debuff getDebuff()
    {
        return new StormDebuff(Target,DebuffDuration);
    }
}
