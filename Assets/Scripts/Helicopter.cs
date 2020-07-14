using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform player;
    public RectTransform playerZone;
    public RectTransform helicopterDeathZone;
    [SerializeField] private float flightSpeed;
    [SerializeField] private int damage = 1;

    private bool isGoingOffScren = false;
    private bool isHoldingPlayer = false;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    //movesToThePlayer
    IEnumerator MoveToPlayer()
    {
        while(Vector3.Distance(transform.position, player.position) >= 1f)
        {
            direction = player.position - transform.position;
            transform.position += direction.normalized * flightSpeed * Time.deltaTime;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }

        StartCoroutine(PickUpAndMovePlayer());
    }

    //picksUpplayer
    IEnumerator PickUpAndMovePlayer()
    {
        player.gameObject.SetActive(false);
        isHoldingPlayer = true;
        /*        Vector3 targetPoint = transform.position;
                targetPoint = Camera.main.WorldToScreenPoint(targetPoint);

                while (RectTransformUtility.RectangleContainsScreenPoint(playerZone, targetPoint))
                {
                    transform.position += direction * flightSpeed * Time.deltaTime;
                    targetPoint = transform.position + direction;
                    targetPoint = Camera.main.WorldToScreenPoint(targetPoint);

                    yield return null;
                }*/
        while (true)
        {
            transform.position += direction * flightSpeed * Time.deltaTime;
            yield return null;
        }
/*
        //leavesPlayer
        ReleasePlayer();*/
    }
    
    //goesOffThescreen
    IEnumerator GoOffTheScreen()
    {

        isGoingOffScren = true;

        while (true)
        {     
            transform.position += direction * flightSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
         
        if(Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q))
        {
            ReleasePlayer();
        }
    }

    private void ReleasePlayer()
    {
        if(isHoldingPlayer)
        {
            StopAllCoroutines();
            player.gameObject.SetActive(true);
            isHoldingPlayer = false;
            player.position = transform.position;
            StartCoroutine(GoOffTheScreen());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            var enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage, true);
            //SFX
        }
        if (collision.gameObject.GetComponent<IHelicopterDeadZone>() && isGoingOffScren)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<IPlayerZone>() && isHoldingPlayer)
        {
            ReleasePlayer();
        }
    }
}
