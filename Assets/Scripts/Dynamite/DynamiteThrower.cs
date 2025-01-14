using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteThrower : MonoBehaviour
{
    public float throwForce = 40;
    public GameObject dynamitePrefab;
    
    public GameObject target;
    private Vector3 objRotation;
    
    private void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ThrowDynamite();
        }

        // Vector3 temp = target.transform.localEulerAngles;
        // temp.z = 0;
        // temp.y = 0;
        // temp.x = transform.localEulerAngles.x + 10;
        // objRotation = temp;
        // transform.eulerAngles = objRotation;
        
    }

    void ThrowDynamite()
    {
        //Vector3 dynamitePos = new Vector3(transform.position.x+2, transform.position.y+3, transform.position.z);
        GameObject dynamite = Instantiate(dynamitePrefab, transform.position, transform.rotation);
        Rigidbody rb = dynamite.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);        
    }
}
