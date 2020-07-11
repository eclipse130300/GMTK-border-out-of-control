using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform player;
    public RectTransform playerZone;
    [SerializeField] private float flightSpeed;
    

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //movesToThePlayer
    IEnumerator MoveToPlayer()
    {
        while(Vector3.Distance(transform.position, player.position) >= 1f)
        {
            direction = player.position - transform.position;
            transform.position += direction.normalized * flightSpeed * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(PickUpAndMovePlayer());
    }

    //picksUpplayer
    IEnumerator PickUpAndMovePlayer()
    {
        player.gameObject.SetActive(false);
        Vector3 targetPoint = transform.position;
        targetPoint = Camera.main.WorldToScreenPoint(targetPoint);

        while (RectTransformUtility.RectangleContainsScreenPoint(playerZone, targetPoint))
        {
            transform.position += direction * flightSpeed * Time.deltaTime;
            targetPoint = transform.position + direction;
            targetPoint = Camera.main.WorldToScreenPoint(targetPoint);

            yield return null;
        }
    }
    
    //leavesPlayer
    //goesOffThescreen
}
