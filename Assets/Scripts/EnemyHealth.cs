﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] AudioClip deathAudio;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die(false);
        }
    }

    public void TakeDamage(int amount, bool playAuido)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die(playAuido);
        }
    }

    private void Die(bool playDeathAFX)
    {
        if (playDeathAFX) SoundManager.Instance.PlayClipAtPoint(deathAudio, transform.position);

        Destroy(gameObject);
    }
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Net>())
        {
            var net = collision.GetComponent<Net>();
            if (!net.isActive) return;

            Debug.Log("Trigger with enemy entered");
            var enemyHelath = GetComponent<EnemyHealth>();
            enemyHelath.TakeDamage(net.damage);
*//*            net.ReduceNetDurability();*//*
        }
    }*/
}
