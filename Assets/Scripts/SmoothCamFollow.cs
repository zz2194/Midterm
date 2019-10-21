using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = target.transform.position + new Vector3(8, 3, 0);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
