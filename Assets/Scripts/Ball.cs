using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    public Camera mainCamera;

    private Vector3 originalPos;

    [SerializeField]
    private BallState ballState = BallState.Pointing;

    [SerializeField]
    float forceMagnitude = 500f;
    [SerializeField]
    float stopDelay;
    float stopTimer;


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
                stopTimer += Time.deltaTime;

                if (stopTimer >= stopDelay && rb.velocity.magnitude < 0.1f)
                {
                    ChangeState(BallState.Pointing);
                    stopTimer = 0f;
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
                    SelectForce();
                    break;
                case BallState.SelectForce:
                    Launch();
                    break;
            }
        }
    }

    private void SelectForce()
    {
        ChangeState(BallState.SelectForce);
    }

    private void Launch()
    {
        ChangeState(BallState.Moving);
        Vector3 direction = mainCamera.transform.forward;
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
