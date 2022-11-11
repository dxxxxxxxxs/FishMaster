using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryself : MonoBehaviour
{
    public float destoryTime;
    private void Start()
    {
        Invoke("DestoryDie",destoryTime);
    }
    void DestoryDie()
    {
        Destroy(gameObject);
    }
}
