using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBTN : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;
    [SerializeField]
    private Text priceText;

    public GameObject TowerPrefab { get => towerPrefab;}
    public Sprite Sprite { get => sprite;}
    public int Price { get => price; set => price = value; }

    private void Start()
    {
        priceText.text = Price + "€";
    }
}
