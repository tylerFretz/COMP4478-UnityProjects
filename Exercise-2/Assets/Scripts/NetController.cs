using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float rotationSpeed = 10;
    private const float minX = -7.5f;
    private const float maxX = 10.5f;
    private const float minY = -8f;
    private const float maxY = 1.75f;


    // Update is called once per frame
    void Update()
    {
        // Handle movement
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float horizontal = 0;
        float vertical = 0;

        if (Input.GetKey(KeyCode.A) && xPos > minX)
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D) && xPos < maxX)
        {
            horizontal = 1;
        }

        if (Input.GetKey(KeyCode.S) && yPos > minY)
        {
            vertical = -1;
        }
        else if (Input.GetKey(KeyCode.W) && yPos < maxY)
        {
            vertical = 1;
        }

        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;

        // Handle rotation
        if (Input.GetKey(KeyCode.Q))
        {
            rb.angularVelocity = rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rb.angularVelocity = -rotationSpeed;
        }
        else
        {
            rb.angularVelocity = 0;
        }
    }

    private bool IsWithinRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
}
