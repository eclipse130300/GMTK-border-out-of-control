using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour, IHaveCooldown
{
/*    public int maxDurability;*/
    public int damage;

/*    [SerializeField] private int currentDurability;*/
    [SerializeField] int iD;
    [SerializeField] float cD;


    public bool isActive = true;

    public int CoolDownId
    {
        get { return iD; } set { iD = value; } 
    }
    public float CoolDownDuration
    {
        get { return cD; }   set { cD = value; }
    }
    public CoolDownSystem coolDownSystem; 
    // Start is called before the first frame update
    void Start()
    {
        coolDownSystem.PutOnCooldown(this);
/*        currentDurability = maxDurability;*/
    }

    private void Update()
    {
        if(!coolDownSystem.IsOnCoolDown(iD))
        {
            DestroyNet();
        }
    }

/*    public void ReduceNetDurability()
    {
        currentDurability--;

        if(currentDurability <= 0)
        {
            DestroyNet();
        }
    }*/

    private void DestroyNet()
    {
        isActive = false;
        //playVFX
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>())
        {
            var enemyScript = collision.GetComponent<EnemyHealth>();
            if (!isActive) return;
            enemyScript.TakeDamage(damage, true);
            /*            ReduceNetDurability();*/
        }
    }
}
