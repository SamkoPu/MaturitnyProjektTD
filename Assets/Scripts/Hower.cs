using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Hower : Singleton<Hower>
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer rangeSpriteRenderer;
    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        FollowMouse();
    }
    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Activate(Sprite sprite)
    {

        this.spriteRenderer.sprite = sprite;
        rangeSpriteRenderer.enabled = true;
        spriteRenderer.enabled = true;
        
        Debug.Log("rendering");
    }
    public void DeActivate()
    {
        spriteRenderer.enabled = false;
        rangeSpriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;
        
    }
}
