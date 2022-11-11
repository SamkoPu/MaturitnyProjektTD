using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{//docasne
    [SerializeField]
    private GameObject towerPrefab;

    public GameObject TowerPrefab { get => towerPrefab; }
}
