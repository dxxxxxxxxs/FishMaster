using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour
{
    public GameObject[] effectPrefabs;
    public void Playeffect()
    {
        foreach (GameObject effectPrefab in effectPrefabs)
        { 
            Instantiate(effectPrefab); 
        }
    }

}
