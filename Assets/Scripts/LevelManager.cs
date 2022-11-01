using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize//property to get size of tiles
    {
        get{return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;}
    }

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        string[] mapData=ReadLevelText();

        //calculate x , then y
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;
        
        
        //calculates corner of screen
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++)//y position
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)//x position
            {
                PlaceTile(newTiles[x].ToString(),x,y,worldStart);
            }
        }
    }

    private void PlaceTile(string tileType,int x, int y,Vector3 worldStart)
    {   //"1"=1
        int tileIndex = int.Parse(tileType);

        //new tile + reference to that tile in newTile
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

        //uses the new tile variable to change the position of the tile
        newTile.transform.position = new Vector3(worldStart.x+(TileSize * x), worldStart.y-(TileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }
}
