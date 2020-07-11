using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpd;
    [SerializeField] StartingPoint[] allStartPoints;
    [SerializeField] EndPoint[] allFinishPoints;

    Vector2 targetMovingPoint;

    private void Start()
    {
        allStartPoints = FindObjectsOfType<StartingPoint>();
        allFinishPoints = FindObjectsOfType<EndPoint>();

        targetMovingPoint = PickRandomStartingPoint();
    }

    private void Update()
    {
        GoToPoint(targetMovingPoint);
    }


    public Vector3 PickRandomStartingPoint()
    {

        var randomNum = Random.Range(0, allStartPoints.Length);

/*        Debug.Log(allStartPoints[randomNum]);*/
        return allStartPoints[randomNum].transform.position;
    }

    public Vector3 PickRandomFinishPoint()
    {

        var randomNum = Random.Range(0, allFinishPoints.Length);

/*        Debug.Log(allFinishPoints[randomNum]);*/
        return allFinishPoints[randomNum].transform.position;
    }

    private void GoToPoint(Vector3 targetPoint)
    {
        Vector3 dir = (targetPoint - transform.position).normalized;
        transform.position += dir * moveSpd * Time.deltaTime;

        if(Vector3.Distance(transform.position , targetPoint) <= 0.05f)
        {
            targetMovingPoint = PickRandomFinishPoint();
        }
    }

}
