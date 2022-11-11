using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSelf : MonoBehaviour
{
    public IEnumerator Hideself(float delay)
    { 
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
