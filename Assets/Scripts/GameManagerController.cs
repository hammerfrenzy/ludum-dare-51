using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float timer = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            //Kill Player
        }
    }

    public void mateReset()
    {
        UnityEngine.Debug.Log("hi");
    }
}
