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
            UnityEngine.Debug.Log("Your BloodLine is Dead");
        }
    }

    public void mateReset()
    {
        timer = 10f;
        snek.resetSnek();
    }
}
