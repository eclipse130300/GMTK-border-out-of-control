using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPdetector : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] int damageToPlayer = 1;

    [SerializeField] AudioClip loseHpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            gameManager.DecreaseHp(damageToPlayer);
            SoundManager.Instance.PlayClipAtPoint(loseHpSound, transform.position);
            Destroy(collision.gameObject);
        }
    }

}
