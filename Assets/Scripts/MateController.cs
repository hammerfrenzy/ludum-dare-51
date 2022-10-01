using DG.Tweening;
using UnityEngine;

public class MateController : MonoBehaviour
{
    GameManagerController gameManager;
    bool mateButton;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        WanderAround();
    }

    private void Update()
    {
        mateButton = Input.GetKey("space");
    }

    // Moves the object to a random point up to 
    // one unit circle distance away, and then
    // starts toward a new target upon completion.  
    private void WanderAround()
    {
        var targetPosition = transform.position + (Vector3)Random.insideUnitCircle;
        transform
            .DOMove(targetPosition, 2f)
            .OnComplete(() =>
            {
                WanderAround();
            });
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
