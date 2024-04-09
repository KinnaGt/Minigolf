using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    Camera mainCamera;

    private Vector3 originalPos;

    [SerializeField]
    private BallState ballState;

    Vector3 initialDistance;
    Vector3 dragDistance;

    void Awake()
    {
        mainCamera = Camera.main;
        ballState = BallState.Pointing;
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    void Update()
    {
        Click();
        ClickUp();
        Stop();
        ResetInput();
    }

    void ResetInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }

    void Stop()
    {
        switch (ballState)
        {
            case BallState.Moving:
                if (rb.velocity.magnitude < 0.1f)
                {
                    ballState = BallState.Pointing;
                }
                break;
        }
    }

    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (ballState)
            {
                case BallState.Pointing:
                    PointBall();
                    break;
            }
        }
    }

    void ClickUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            switch (ballState)
            {
                case BallState.SelectForce:
                    SelectForce();
                    break;
            }
        }
    }

    private void PointBall()
    {
        ballState = BallState.SelectForce;
        initialDistance = Input.mousePosition;
    }

    private void SelectForce()
    {
        ballState = BallState.Moving;

    }

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
