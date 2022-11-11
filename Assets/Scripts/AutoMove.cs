using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 dir = Vector3.right;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
    }
     
}
