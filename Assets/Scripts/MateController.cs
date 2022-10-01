using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateController : MonoBehaviour
{
    GameManagerController gameManager;
    bool mateButton;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }

    private void Update()
    {
        mateButton = Input.GetKey("space");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        UnityEngine.Debug.Log(collision);
        SnekController snek = collision.GetComponent<SnekController>();
        if (mateButton && snek != null)
        {
            // Display Mate Prompt
            UnityEngine.Debug.Log("mate");
            gameManager.mateReset();
        }
    }
}
