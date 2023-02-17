using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { STORM,FIRE,FROST,POISON,NONE}

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    private string projectileType;
    [SerializeField]
    private float projectileSpeed;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    private Animator myAnimator;

    private SpriteRenderer mySpriteRenderer;

    private Monster target;
    public Monster Target
    {
        get { return target; }
    }

    
    [SerializeField]
    private int damage;
    [SerializeField]
    private float debuffDuration;

    [SerializeField]
    private float proc;
    public Element ElementType { get; protected set; }
    public int Price { get; set; }
    public int Damage { get => damage; }
    public float DebuffDuration { get => debuffDuration; set => debuffDuration = value; }
    public float Proc { get => proc; set => proc = value; }

    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;
    private float attackTimer;
    [SerializeField]
    private float attackCooldown;



    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator= transform.parent.GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
        Debug.Log(target);
    }
    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer>=attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if (target==null&&monsters.Count>0)
        {
            target = monsters.Dequeue();
        }
        if (target !=null&& target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                myAnimator.SetTrigger("Attack");
                
                canAttack = false;
            }
        }
        else if (monsters.Count>0)
        {
            target = monsters.Dequeue();
        }
        if (target!=null&&!target.Alive)
        {
            target = null;
        }
    }
    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Initialize(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Monster")
        {
            monsters.Enqueue(other.GetComponent<Monster>());    
        }
    }

    public abstract Debuff getDebuff();

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            target = null;
        }
    }
}

