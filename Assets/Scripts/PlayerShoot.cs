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

    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] missSounds;

    private SoundManager soundManager;
    private Animator animator;

    public int CoolDownId => iD;
    public float CoolDownDuration => cdDuration;
    [SerializeField] CoolDownSystem cdSystem;

    private PlayerAim playerAim;

    private void Awake()
    {
        playerAim = GetComponent<PlayerAim>();
        animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
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
        animator.SetTrigger("Attack");


        var colliders = Physics2D.OverlapCircleAll(transform.position + playerAim.aimDirection * damageRadius, damageRadius);
        
        foreach(Collider2D collider in colliders)
        {
            if(collider.GetComponent<EnemyHealth>())
            {
                var enemy = collider.GetComponent<EnemyHealth>();
                enemy.TakeDamage(damage);

                //play hit audio
                soundManager.PlayRandomClipAtPoint(hitSounds, transform.position);
            }
            else
            {
                //play miss audio
                soundManager.PlayRandomClipAtPoint(missSounds, transform.position);
            }
        }
        if(colliders.Length == 0)
        {
            //play miss audio
            soundManager.PlayRandomClipAtPoint(missSounds, transform.position);
        }
    }
}
