using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;
    public Transform[] genPositions;
    public GameObject[] fishPrefabs;
    public float fishGenWaitTime = 0.5f;
    public float waveGenWaitTime = 0.3f;
    void Start()
    {
        InvokeRepeating("MakeFishes",0,waveGenWaitTime);
    }
    public void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPositions.Length);                     //鱼的生成位置
        int fishPreIndex = Random.Range(0,fishPrefabs.Length);                      //生成哪种鱼
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;     //获取鱼的最大生成数量
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed; //获取鱼的最大速度
        int num = Random.Range((maxNum / 2) + 1, maxNum);                           //本次生成多少数量的鱼
        int speed = Random.Range(maxSpeed/2,maxSpeed);                              //本次生成的鱼速度
        int moveType=Random.Range(0,2);                                             //获取鱼的移动模式，0：直走，1：转弯
        int angOffset;                                                              //仅直走生效，直走的倾斜角
        int angSpeed;                                                               //仅转弯生效，转弯的角速度
        if (moveType==0)
        {
            angOffset=Random.Range(-22,22);
            StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));

        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(-15,-9);
            }
            else 
            {
                angSpeed = Random.Range(9,15);               
            }
            StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }
    }
    IEnumerator GenStraightFish(int genPosIndex, int fishPreIndex, int num, int speed, int angOffset)
    {
        for (int i = 0;  i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder,false);
            fish.transform.localPosition = genPositions[genPosIndex].localPosition;
            fish.transform.localRotation = genPositions[genPosIndex].localRotation;
            fish.transform.Rotate(0,0,angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<AutoMove>().speed=speed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }
    IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, int speed, int angspeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPositions[genPosIndex].localPosition;
            fish.transform.localRotation = genPositions[genPosIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<AutoMove>().speed = speed;
            fish.AddComponent<AutoRotate>().speed=angspeed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }
    
}
