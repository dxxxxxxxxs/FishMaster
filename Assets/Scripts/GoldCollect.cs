using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        { 
            Destroy(collision.gameObject);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.goldClip);
        }
    }
}
