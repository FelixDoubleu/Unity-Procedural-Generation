using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public Transform target;
    public int speed = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}
