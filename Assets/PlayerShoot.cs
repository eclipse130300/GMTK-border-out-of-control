using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour, IHaveCooldown
{
    [SerializeField] int iD;
    [SerializeField] float cdDuration;
    [SerializeField] int damage;
    [SerializeField] float damageRadius;

    public int CoolDownId => iD;
    public float CoolDownDuration => cdDuration;
    [SerializeField] CoolDownSystem cdSystem;

    private PlayerAim playerAim;

    private void Awake()
    {
        playerAim = GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !cdSystem.IsOnCoolDown(iD))
        {
            Hit();
            cdSystem.PutOnCooldown(this);
        }
    }

    private void Hit()
    {
        //play animation


        var colliders = Physics2D.OverlapCircleAll(transform.position + playerAim.aimDirection * damageRadius, damageRadius);
        
        foreach(Collider2D collider in colliders)
        {
            if(collider.GetComponent<EnemyHealth>())
            {
                var enemy = collider.GetComponent<EnemyHealth>();
                enemy.TakeDamage(damage);
            }
        }
    }

/*    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + playerAim.aimDirection * damageRadius, damageRadius);

        Gizmos.color = Color.red; 
    }*/
}
