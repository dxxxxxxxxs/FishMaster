using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWave : MonoBehaviour
{
    private Vector3 temp;
    private void Start()
    {
        temp = -transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,temp,10*Time.deltaTime);
    }
}
