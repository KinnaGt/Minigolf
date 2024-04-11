using UnityEngine;

public static class Utilities
{
    public static Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar =
            new(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear =
            new(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        if (
            Physics.Raycast(
                worldMousePosNear,
                worldMousePosFar - worldMousePosNear,
                out hit,
                float.PositiveInfinity
            )
        )
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}
