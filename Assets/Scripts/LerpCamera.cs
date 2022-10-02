using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    private Transform cameraTarget;
    private float speed = 5f;
    private float cameraZ = -10f;
    public bool disableCameraMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = FindObjectOfType<SnekController>().transform;
    }

    // FixedUpdate for camera lerping
    void FixedUpdate()
    {
        if (disableCameraMovement) return;

        var newPosition = Vector2.Lerp(
            (Vector2)transform.position,
            (Vector2)cameraTarget.position,
            speed * Time.deltaTime
        );

        transform.position = new Vector3(newPosition.x, newPosition.y, cameraZ);
    }

    public void SnapToPosition(Vector3 position)
    {
        var newCameraPosition = new Vector3(position.x, position.y, -10);
        transform.position = newCameraPosition;
    }
}
