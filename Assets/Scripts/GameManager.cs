using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{
    public event CurrencyChanged Changed;

    public TowerBTN ClickedBtn { get; set; }

    private int currency;
    private int wave = 0;

    private int lives;
    [SerializeField]
    private Text livesTxt;


    public int Lives
    {
        get { return lives; }
        set 
        {
            this.lives = value;
            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }
            livesTxt.text = lives.ToString();
        }
    }

    private bool gameOver = false;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private GameObject statsPanel;

    [SerializeField]
    private Text sellText;

    [SerializeField]
    private Text statText;


    private Tower selectedTower;


    [SerializeField]
    private Text waveTxt;
    
    [SerializeField]
    private Text currencyText;
    [SerializeField]
    private GameObject waveBtn;

    private List<Monster> activeMonsters = new List<Monster>();
    public ObjectPool Pool { get; set; }
    public bool WaveActive
    {
        get { return activeMonsters.Count > 0; }
    }
    public int Currency
    {
        get { return currency; }
        set 
        { 
            this.currency = value; 
            this.currencyText.text = value.ToString() + "<color=lime>€</color>";

            OnCurrencyChanged();

        }

    }
    private int health=15;
    private void Awake()
    {
            Pool=GetComponent<ObjectPool>();
    }

    private void Start()
    {
        Lives = 10;
        Currency = 5;
    }


    private void Update()
    {
        HandleEscape();
    }
    public void PickTower(TowerBTN towerBTN)
    {
        if (Currency>=towerBTN.Price&&!WaveActive)
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

    public void OnCurrencyChanged()
    {
        if (Changed!=null)
        {
            Changed();
        }
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower !=null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();

        sellText.text = "+ " + (selectedTower.Price / 2).ToString();
        upgradePanel.SetActive(true);
    }

    public void DeselectTower()
    {
        if (selectedTower!=null)
        {
            selectedTower.Select();
        }
        upgradePanel.SetActive(false);
        selectedTower = null;
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
        wave++;

        waveTxt.text = string.Format("Wave: <color=cyan>{0}</color>", wave);
 

        StartCoroutine(SpawnWave());
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        for (int i = 0; i < wave; i++)
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

            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn(health);

            if (wave%3==0)
            {
                health += 5;
            }

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
    }
    public void removeMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive&&!gameOver)
        {
            waveBtn.SetActive(true);
        }
    }

    
    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SellTower()
    {
        if (selectedTower!=null)
        {
            Currency += selectedTower.Price / 2;
            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;
            selectedTower.GetComponentInParent<TileScript>().WalkAble = true;
            Destroy(selectedTower.transform.parent.gameObject);
            DeselectTower();
        }

    }

    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }
    public void SetTooltipText(string txt)
    {
        statText.text = txt;
        
    }

    public void ShowSelectedTowerStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        UpdateUpgradeTooltip();
    }

    public void UpdateUpgradeTooltip()
    {
        if (selectedTower!=null)
        {
            SetTooltipText(selectedTower.GetStats());
        }
    }
}
