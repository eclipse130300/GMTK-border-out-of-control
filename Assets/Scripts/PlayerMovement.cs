using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RectTransform playerZone;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpd = 5f;
    [SerializeField] private float Ymax;
    [SerializeField] private float Ymin;
    [SerializeField] private float xRange;
    Vector2 movement;
    /*public Vector3[] corners = new Vector3[4];*/

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        /*playerZone.GetWorldCorners(corners);*/
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = Vector2.ClampMagnitude(movement, 1f);
    }

    private void FixedUpdate()
    {

        Vector2 clumpedPos = Vector3.zero;
        clumpedPos.x = Mathf.Clamp(rb.position.x, -xRange, xRange);
        clumpedPos.y = Mathf.Clamp(rb.position.y, Ymin, Ymax);


        rb.MovePosition(clumpedPos + movement * moveSpd * Time.fixedDeltaTime);
    }
}
