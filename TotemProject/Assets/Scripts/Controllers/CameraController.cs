using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [HideInInspector] private Vector3 nextPosition;

    public void SetNewPosition(Vector3 pos)
    {
        nextPosition = pos;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, nextPosition, .25f);
    }

}
