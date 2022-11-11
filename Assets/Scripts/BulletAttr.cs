using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttr : MonoBehaviour
{
    public int speed;
    public int damage;
    public GameObject WebPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Fish")
        {
            Destroy(gameObject);
            GameObject web = Instantiate(WebPrefab);
            web.transform.SetParent(gameObject.transform.parent,false);
            web.transform.position=gameObject.transform.position;
            web.GetComponent<WebAttr>().damage = damage;

        }
    }
}
