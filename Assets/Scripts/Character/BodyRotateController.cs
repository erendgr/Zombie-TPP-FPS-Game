using System;
using UnityEngine;

public class BodyRotateController : MonoBehaviour
{
    public Transform _camera;


    private void Start()
    {
    }


    private void LateUpdate()
    {
        Vector3 temp = _camera.transform.localEulerAngles;
    }
}