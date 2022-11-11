using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get { return _instance; }
    }
    public int lv=1;
    public int gold = 500;
    public int exp=0;
    public int bgIndex=0;
    public const int bigCountdown = 240;
    public const int smallCountdown = 60;
    public float bigTimer = bigCountdown;
    public float smallTimer = smallCountdown;

    public Text lvText;
    public Text lvNameText;
    public Text smallCountdownText;
    public Text bigCountdownText;
    public Text goldText;
    public Text oneShootCostText;
    public Button bigCountdownButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;
    public Color goldColor;
    public Image bgImage;

    public GameObject lvUpTips;
    public GameObject changeGunEffect;
    public GameObject lvEffect;
    public GameObject goldEffect;
    public GameObject seaWaveEffect;
    public Sprite[] bgSprites;
    

    public Transform bulletHolder;
    public GameObject[] bullet1Cos;
    public GameObject[] bullet2Cos;
    public GameObject[] bullet3Cos;
    public GameObject[] bullet4Cos;
    public GameObject[] bullet5Cos;
    public GameObject[] gunGos;

    private int costIndex = 0;
    //每一炮所需的金币数和造成的伤害值
    private int[] oneShootCosts = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private string[] lvName = {"新手","入门","青铜","白银","黄金","铂金","钻石","大师","宗师","王者"};
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold",gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        smallTimer = PlayerPrefs.GetFloat("scd", smallTimer);
        bigTimer = PlayerPrefs.GetFloat("bcd", bigTimer);
        exp = PlayerPrefs.GetInt("exp", exp);
        UpdateUI();
    }
    private void Update()
    {
        ChangeBulletCost();
        Fire();
        UpdateUI();
        ChangeBg();
    }
    void ChangeBg()
    {
        if (bgIndex != lv / 20)
        { 
            bgIndex=lv/ 20;
            Instantiate(seaWaveEffect);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seaWaveClip);
            if (bgIndex >= 3)
            {
                bgImage.sprite = bgSprites[3];
            }
            else
            {
                bgImage.sprite = bgSprites[bgIndex];
            }
            
        }
    }
    void UpdateUI()
    {
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer <= 0)
        {
            gold += 50;
            smallTimer = smallCountdown;
        }
        if (bigTimer <= 0 &&bigCountdownButton.gameObject.activeSelf==false)
        { 
            bigCountdownText.gameObject.SetActive(false);
            bigCountdownButton.gameObject.SetActive(true);
        }
        //升级公式：每一级升级所需经验=1000+200*当前等级。
        while (exp>=1000+200*lv)
        {
            exp = exp - (1000 + 200 * lv);
            lv++;
            lvUpTips.SetActive(true);
            lvUpTips.transform.Find("Text").GetComponent<Text>().text=lv.ToString();
            StartCoroutine(lvUpTips.GetComponent<HideSelf>().Hideself(0.6f));
            Instantiate(lvEffect);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.lvUpClip);
        }
        goldText.text = "$" + gold;
        lvText.text = lv.ToString();
        if ((lv / 10) <= 9)
        {
            lvNameText.text = lvName[lv / 10];
        }
        else
        {
            lvNameText.text = lvName[9];
        }
        smallCountdownText.text = " "+(int)smallTimer/10+"  "+(int)smallTimer%10;
        bigCountdownText.text = (int)bigTimer + "s";
        expSlider.value = ((float)exp) / (1000 + (200 * lv));
    }
    public void Fire()
    {
        GameObject[] useBullets=bullet5Cos;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0)&& EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (gold - oneShootCosts[costIndex] >= 0)
            {
                switch (costIndex / 4)
                {
                    case 0: useBullets = bullet1Cos; break;
                    case 1: useBullets = bullet2Cos; break;
                    case 2: useBullets = bullet3Cos; break;
                    case 3: useBullets = bullet4Cos; break;
                    case 4: useBullets = bullet5Cos; break;
                }
                bulletIndex = (lv % 10 >= 9) ? 9 : lv % 10;
                gold -= oneShootCosts[costIndex];
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fireClip);
                GameObject bullet = Instantiate(useBullets[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = gunGos[costIndex / 4].transform.rotation;
                bullet.GetComponent<BulletAttr>().damage = oneShootCosts[costIndex];
                bullet.AddComponent<AutoMove>().dir = Vector3.up;
                bullet.GetComponent<AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
            }
            else 
            {
                StartCoroutine(GoldNotEnough());
            }
        }
    }
    public void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonMDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }
    }
    public void OnButtonPDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex++;
        Instantiate(changeGunEffect);
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeGunClip);
        costIndex = (costIndex > oneShootCosts.Length - 1) ? 0 : costIndex;
        gunGos[(costIndex / 4)].SetActive(true);
        oneShootCostText.text ="$"+ oneShootCosts[costIndex];
    }
    public void OnButtonMDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex--;
        Instantiate(changeGunEffect);
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeGunClip);
        costIndex = (costIndex < 0) ? oneShootCosts.Length-1 : costIndex;
        gunGos[(costIndex / 4)].SetActive(true);
        oneShootCostText.text = "$" + oneShootCosts[costIndex]; 
    }
    public void OnBigCountDownButtonDown()
    {
        bigCountdownButton.gameObject.SetActive(false);
        bigCountdownText.gameObject.SetActive(true);
        bigTimer = bigCountdown;
        gold += 500;
        Instantiate(goldEffect);
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
    }
    IEnumerator GoldNotEnough()
    {
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        goldText.color = goldColor;
    }
}
