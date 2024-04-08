using System.Drawing;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    Camera mainCamera;

    private Vector3 originalPos;

    [SerializeField]
    private BallState ballState;

    void Awake()
    {
        mainCamera = Camera.main;
        ballState = BallState.Pointing;
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    void Update()
    {
        switch (ballState)
        {
            case BallState.Pointing:
                PointBall();
                break;
            case BallState.SelectForce:
                SelectForce();
                break;
            case BallState.Moving:
                break;
        }
    }

    private void PointBall() { }

    private void SelectForce() { }

    private void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = originalPos;
    }
}

public enum BallState
{
    Idle,
    Pointing,
    SelectForce,
    Moving,
}
