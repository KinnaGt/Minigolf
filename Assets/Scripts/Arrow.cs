using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRen;

    [SerializeField]
    Ball ball;

    [SerializeField]
    Vector3 offsetPos = new Vector3(0, 0, 0);

    void Awake()
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.OnBallStateChange += ChangeState;
    }

    void Update()
    {
        Vector3 direction =
            new(ball.mainCamera.transform.forward.x, 0, ball.mainCamera.transform.forward.z);
        Debug.Log("camera direction: " + direction);
        transform.position = ball.transform.position + direction + offsetPos;
    }

    private void ChangeState(BallState ballState)
    {
        switch (ballState)
        {
            case BallState.Pointing:
                spriteRen.enabled = true;
                break;
            case BallState.SelectForce:
                //TODO change sprite
                spriteRen.enabled = true;
                break;
            default:
                spriteRen.enabled = false;
                break;
        }
    }
}
