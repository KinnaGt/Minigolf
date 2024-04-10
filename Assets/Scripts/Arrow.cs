using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRen;

    [SerializeField]
    Ball ball;

    [SerializeField]
    Vector3 offsetPos;

    void Awake()
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.OnBallStateChange += ChangeState;
    }

    void Update()
    {
        Position();
    }

    void Position()
    {
        transform.position = ball.transform.position + offsetPos;
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
