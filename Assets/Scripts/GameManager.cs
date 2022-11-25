using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerBTN ClickedBtn { get; set; }

    private void Update()
    {
        HandleEscape();
    }
    public void PickTower(TowerBTN towerBTN)
    {
        this.ClickedBtn = towerBTN;
        Hower.Instance.Activate(towerBTN.Sprite);
    }

    public void BuyTower()
    {
        Hower.Instance.DeActivate();
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hower.Instance.DeActivate();
        }
    }
}
