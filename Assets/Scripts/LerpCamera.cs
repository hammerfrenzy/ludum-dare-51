using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    private Transform cameraTarget;
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = FindObjectOfType<SnekController>().transform;
    }

    // FixedUpdate for camera lerping
    void FixedUpdate()
    {
        var newPosition = Vector2.Lerp(
            (Vector2)transform.position,
            (Vector2)cameraTarget.position,
            speed * Time.deltaTime
        );

        transform.position = new Vector3(newPosition.x, newPosition.y, -10);
    }
}
