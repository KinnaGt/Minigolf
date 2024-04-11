using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    bool showLine = false;

    private void Awake()
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.OnBallStateChange += ChangeState;
    }

    private void ChangeState(BallState ballState)
    {
        switch (ballState)
        {
            case BallState.SelectForce:
                showLine = true;
                lineRenderer.enabled = true;
                break;
            default:
                lineRenderer.enabled = false;
                showLine = false;
                break;
        }
    }

    private void Update()
    {
        if (!showLine)
            return;

        Vector3? worldPoint = Utilities.CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(worldPoint.Value);
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = { transform.position, worldPoint };

        lineRenderer.SetPositions(positions);

        lineRenderer.enabled = true;
    }

    public Vector3[] GetLinePoints()
    {
        if (lineRenderer.positionCount < 2)
            return null;

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        return points;
    }
    
}
