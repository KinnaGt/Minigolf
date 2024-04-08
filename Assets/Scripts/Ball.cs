using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject arrowPrefab; // Prefab for the arrow object
    public float arrowDistance = 2f; // Distance in front of the ball to display the arrow
    public float cameraOffset = 2f; // Offset for camera movement

    private GameObject arrowInstance; // Reference to the arrow object

    void Start() { }

    void Update() { }
}

public enum BallState
{
    Pointing,
    SelectForce,
    Moving,
}
