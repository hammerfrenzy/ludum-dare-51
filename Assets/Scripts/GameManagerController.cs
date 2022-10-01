using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float timer = 10f;
    SnekController snek;
    void Start()
    {
        snek = GameObject.Find("Snek").GetComponent<SnekController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            UnityEngine.Debug.Log("Your bloodline is dead");
        }
    }

    public void mateReset()
    {
        // Player successfully found a mate
        timer = 10f;
        snek.resetSnek();
    }
}
