using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform followObject;

    [SerializeField]
    Vector3 offset = new Vector3(0, 2, -5); // Desplazamiento detrás del objeto

    void Update()
    {
        if (followObject != null)
        {
            transform.position = followObject.position + offset;
            transform.LookAt(followObject);
        }
    }
}
