using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : Tower
{
    private void Start()
    {
        ElementType = Element.FROST;
    }
    public override Debuff getDebuff()
    {
        return new FrostDebuff(Target);
    }
}
