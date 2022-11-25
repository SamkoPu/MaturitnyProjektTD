using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hower : Singleton<Hower>
{
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        spriteRenderer.enabled = true;

    }
    public void DeActivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;
    }
}
