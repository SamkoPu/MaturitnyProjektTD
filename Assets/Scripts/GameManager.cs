using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerBTN ClickedBtn { get; set; }

    private int currency;
    [SerializeField]
    private Text currencyText;
    public ObjectPool Pool { get; set; }
    public int Currency
    {
        get { return currency; }
        set 
        { 
            this.currency = value; 
            this.currencyText.text = value.ToString() + "<color=lime>€</color>";
        }

    }

    private void Awake()
    {
            Pool=GetComponent<ObjectPool>();
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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        int monsterIndex = Random.Range(0, 4);

        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "BlueMonster";
                break;
            case 1:
                type = "RedMonster";
                break;
            case 2:
                type = "GreenMonster";
                break;
            case 3:
                type = "PurpleMonster";
                break;
        }

        Monster monster=Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();


        yield return new WaitForSeconds(2.5f);
    }
}
