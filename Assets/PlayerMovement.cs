using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpd = 5f;
    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.MovePosition(rb.position + movement * moveSpd * Time.fixedDeltaTime);
    }
}
