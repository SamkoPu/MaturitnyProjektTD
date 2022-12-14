using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool WalkAble { get; set; }
    public bool Debugging { get; set; }
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        WalkAble = true;
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty&&Debugging==false)
            {
                ColorTIle(emptyColor);
            }
            if (!IsEmpty && Debugging == false)
            {
                ColorTIle(fullColor);
            }
            else if (Input.GetMouseButton(0))
            {
                PlaceTower();
            }
        }
    }
    private void OnMouseExit()
    {
        if (Debugging == false)
        {
            ColorTIle(Color.white);
        }
    }


    private void PlaceTower()
    {
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, (quaternion.identity));
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        IsEmpty = false;
        ColorTIle(Color.white);

        GameManager.Instance.BuyTower();

        WalkAble = false;
    }

    private void ColorTIle(Color32 newColor)
    {
        spriteRenderer.color = newColor;

    }

}
