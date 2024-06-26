using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    public Camera mainCamera;

    private Vector3 originalPos;

    [SerializeField]
    private LineForce lineForce;

    [SerializeField]
    private BallState ballState = BallState.Pointing;

    [SerializeField]
    float factor = 500f;
    float forceMagnitude;

    [SerializeField]
    float stopDelay;
    float stopTimer;

    [SerializeField]
    float maxForce;

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
        if (Input.GetMouseButtonDown(0))
        {
            switch (ballState)
            {
                case BallState.Pointing:
                    SelectForce();
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
                    ChangeState(BallState.Moving);
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
        Vector3[] linePoints = lineForce.GetLinePoints();
        if (linePoints != null && linePoints.Length >= 2)
        {
            Vector3 launchDirection = (linePoints[1] - linePoints[0]).normalized;
            float distance = Vector3.Distance(linePoints[0], linePoints[1]);
            forceMagnitude = distance * factor; 
            forceMagnitude = Mathf.Clamp(forceMagnitude, 0f, maxForce);
            LaunchObject(launchDirection);
        }
    }

    private void LaunchObject(Vector3 launchDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            launchDirection = new Vector3(launchDirection.x, 0f, launchDirection.z).normalized;

            Vector3 force = -launchDirection.normalized * forceMagnitude;
            force.y = 0f;
            rb.AddForce(force, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            NextLevel();
        }
        else if (other.CompareTag("Floor"))
        {
            ResetBall();
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
