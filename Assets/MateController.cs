using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateController : MonoBehaviour
{
    public GameManagerController gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.mateReset();
    }
}
