using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (followObject != null)
        {
            if (Input.GetMouseButton(0))
            {
                currentAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            }

            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
            transform.position = followObject.position + rotation * offset;

            transform.LookAt(followObject);
        }
    }
}
