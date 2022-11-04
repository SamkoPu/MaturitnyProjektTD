using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    private Point blueSpawn,redSpawn;

    [SerializeField]
    private GameObject bluePortalPrefab, redPortalPrefab;

    public Dictionary<Point,TileScript> Tiles { get; set; }


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
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData=ReadLevelText();

        //calculate x , then y
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;
        
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
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x+TileSize,maxTile.y-TileSize));

        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {   //"1"=1
        int tileIndex = int.Parse(tileType);

        //new tile + reference to that tile in newTile
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y),new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));
    }


    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 1);
        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

        redSpawn = new Point(14, 1);
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}
