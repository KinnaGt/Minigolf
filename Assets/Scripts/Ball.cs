using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    Camera mainCamera;

    private Vector3 originalPos;

    [SerializeField]
    private BallState ballState;

    [SerializeField]
    float forceMagnitude = 500f;

    public delegate void BallStateHandler(BallState ballState);
    public event BallStateHandler OnBallStateChange;

    #region Events
    public void ChangeState(BallState ballState)
    {
        this.ballState = ballState;
        OnBallStateChange?.Invoke(ballState);
    }
    
    #endregion

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
                    ChangeState(BallState.Pointing);
                }
                break;
        }
    }

    void Click()
    {
        if (Input.GetKeyDown(KeyCode.W))
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
        ChangeState(BallState.SelectForce);
    }

    private void SelectForce()
    {
        ChangeState(BallState.Moving);
        // Calcula la dirección en la que la cámara está mirando
        Vector3 direction = mainCamera.transform.forward;
        Debug.Log("Direction: " + direction);
        direction.y = 0;

        rb.AddForce(direction * forceMagnitude);
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
