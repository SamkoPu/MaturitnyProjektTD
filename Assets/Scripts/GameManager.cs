using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerBTN ClickedBtn { get; set; }

    private int currency;
    [SerializeField]
    private Text currencyText;

    public int Currency
    {
        get { return currency; }
        set 
        { 
            this.currency = value; 
            this.currencyText.text = value.ToString() + "<color=lime>€</color>";
        }

    }
    private void Start()
    {
        Currency = 5;
    }


    private void Update()
    {
        HandleEscape();
    }
    public void PickTower(TowerBTN towerBTN)
    {
        if (Currency>=towerBTN.Price)
        {
            this.ClickedBtn = towerBTN;
            Hower.Instance.Activate(towerBTN.Sprite);
        }
        
    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hower.Instance.DeActivate();
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hower.Instance.DeActivate();
        }
    }
}
