using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform followObject;

    [SerializeField]
    Vector3 offset = new Vector3(0, 2, -5);

    float currentAngle = 0f;

    [SerializeField]
    float rotationSpeed = 5f;

    private bool blocked;

    void Awake()
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.OnBallStateChange += ChangeState;
    }

    private void ChangeState(BallState ballState)
    {
        switch (ballState)
        {
            case BallState.Pointing:
                blocked = false;
                break;
            default:
                blocked = true;
                break;
        }
    }

    void Update()
    {
        if (followObject != null && !blocked)
        {
            if (Input.GetMouseButton(0))
            {
                currentAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            }
        }
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        transform.position = followObject.position + rotation * offset;

        transform.LookAt(followObject);
    }
}
