using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    [Range(0.1f, 0.9f)]
    public float smoothing = 0.9f;

    private Vector3 offset;



    private void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        var targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, 1 - smoothing);
        
    }



}
