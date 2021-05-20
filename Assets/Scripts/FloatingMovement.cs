using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    private float floatingTimer = 0f;
    public float floatingMax = 1.25f;
    public float floatingDirection = 0.1f;

    private void FixedUpdate()
    {
        if (floatingTimer < floatingMax)
        {
            transform.position = new Vector2(transform.position.x, 
                transform.position.y + (Time.fixedDeltaTime * floatingDirection));
            floatingTimer += Time.fixedDeltaTime;
        }
        else
        {
            floatingDirection *= -1;
            floatingTimer = 0f;
        }
    }
}
