using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset;
    private void Start()
    {
        if (Target == null) return;

        offset = transform.position - Target.position;
    }

    private void Update()
    {
        if(Target == null) return;
        Vector3 pos = Target.transform.position;
        pos += offset;
        transform.position = pos;
    }
}
