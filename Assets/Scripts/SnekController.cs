using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    float disableMovementTimer = 0.5f;
    bool disableMovement = false;


    // Traits
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(disableMovement)
        {
            disableMovementTimer -= Time.deltaTime;
            if(disableMovementTimer < 0)
            {
                disableMovement = false;
                disableMovementTimer = 0.5f;
            }
        }
    }

    void FixedUpdate()
    {
        if (disableMovement) return;
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void resetSnek()
    {
        disableMovement = true;
        Vector2 position = new Vector2(0, 0);
        rigidbody2d.MovePosition(position);
    }
}
