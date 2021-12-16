using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovLogger : MonoBehaviour
{
    public float fov, distance, aspect;


    private void OnDrawGizmos()
    {
        if (fov == 0 || distance == 0 || aspect == 0) return;

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawFrustum(Vector3.zero, fov, distance, 0, aspect);
    }
}
