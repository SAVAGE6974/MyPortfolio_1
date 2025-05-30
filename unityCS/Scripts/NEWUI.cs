using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NEWUI : MonoBehaviour
{
    [SerializeField] GameObject gamemanager;

    public static NEWUI instance;

    public Text creditText;
    public Text UpCountdownText;
    public Text AL1S_bulletText;
    public Text AL1S_sideBulletText;
    public Text Wakamo_bulletText;
    public Text Wakamo_sideBulletText;
    public Text AL1S_hpText;
    public Text Wakamo_hpText;

    public GameObject winAnimation;
    public GameObject al1SSideBarPanel;
    public GameObject wakamoSideBarPanel;
    public GameObject al1SBuyPanel;
    public GameObject wakamoBuyPanel;
    public GameObject BuyphasePannel;

    private bool isOpenStoreScreen;
    public static bool isBuyPanelInteractable = true;

    public static bool canFire = true;
    public static bool canGoAttack = false;

    private int AL1S_bulletCount = 8;
    public static int AL1S_sideBulletCount = 0;
    private int Wakamo_bulletCount = 8;
    public static int Wakamo_sideBulletCount = 0;

    public static float _hp = 100f;

    private void Awake()
    {
        winAnimation.SetActive(false);
        
        instance = this;

        al1SSideBarPanel.SetActive(false);
        wakamoSideBarPanel.SetActive(false);
        al1SBuyPanel.SetActive(false);
        wakamoBuyPanel.SetActive(false);
        BuyphasePannel.SetActive(true);

        if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            Destroy(Wakamo_bulletText);
            AL1S_bulletText.text = AL1S_bulletCount.ToString();
        }
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            Destroy(AL1S_bulletText);
            Wakamo_bulletText.text = Wakamo_bulletCount.ToString();
        }
    }

    private void Start()
    {
        if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            al1SSideBarPanel.SetActive(true);
            wakamoSideBarPanel.SetActive(false);
        }
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            wakamoSideBarPanel.SetActive(true);
            al1SSideBarPanel.SetActive(false);
        }

        StartCoroutine(StartCountdownCycle(3));
    }

    private void Update()
    {
        if (isBuyPanelInteractable)
        {
            OpenNCloseBuyPanel();
        }
        AL_1S_reload();
    }

    public void UpdateHP()
    {
        if (LockinManager.lastSelectedCharacter == "AL-1S")
            AL1S_hpText.text = _hp.ToString();
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
            Wakamo_hpText.text = _hp.ToString();
    }

    public void AL_1S_buyBullet()
    {
        AL1S_sideBulletCount += 10;
        AL1S_sideBulletText.text = AL1S_sideBulletCount.ToString();
    }

    private void AL_1S_reload()
    {
        if (LockinManager.lastSelectedCharacter == "AL-1S" && Input.GetKeyDown(KeyCode.R))
        {
            AL1S_sideBulletCount -= 10;
            AL1S_bulletCount += 10;
            NEWSampleLaserClick.AL1SgunbulletNum = AL1S_bulletCount;

            AL1S_bulletText.text = AL1S_bulletCount.ToString();
            AL1S_sideBulletText.text = AL1S_sideBulletCount.ToString();
        }
    }

    public void buy_hp()
    {
        if (NEWBuyManager.credits - 100 >= 0)
        {
            _hp = 100;
            Debug.Log("u buy hp");
            NEWBuyManager.credits -= 100;
            creditText.text = NEWBuyManager.credits.ToString();

            if (LockinManager.lastSelectedCharacter == "AL-1S")
                AL1S_hpText.text = _hp.ToString();
            else if (LockinManager.lastSelectedCharacter == "Wakamo")
                Wakamo_hpText.text = _hp.ToString();
        }
    }

    public void HitTower()
    {
        _hp--;
        if (LockinManager.lastSelectedCharacter == "AL-1S")
            AL1S_hpText.text = _hp.ToString();
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
            Wakamo_hpText.text = _hp.ToString();
    }

    public void UseBullet()
    {
        if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            if (AL1S_bulletCount > 0)
            {
                AL1S_bulletCount--;
                AL1S_bulletText.text = AL1S_bulletCount.ToString();
            }
        }
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            if (Wakamo_bulletCount > 0)
            {
                Wakamo_bulletCount--;
                Wakamo_bulletText.text = Wakamo_bulletCount.ToString();
            }
        }
    }

    private void OpenNCloseBuyPanel()
    {
        if (Input.GetKeyDown(KeyCode.B) && isOpenStoreScreen == false)
        {
            Debug.Log("Open buy panel");

            BuyphasePannel.SetActive(false);
            isOpenStoreScreen = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            canFire = false;

            if (LockinManager.lastSelectedCharacter == "AL-1S")
                al1SBuyPanel.SetActive(true);
            else if (LockinManager.lastSelectedCharacter == "Wakamo")
                wakamoBuyPanel.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)) && isOpenStoreScreen)
        {
            Debug.Log("Close buy panel");

            BuyphasePannel.SetActive(true);
            isOpenStoreScreen = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            canFire = true;

            if (LockinManager.lastSelectedCharacter == "AL-1S")
                al1SBuyPanel.SetActive(false);
            else if (LockinManager.lastSelectedCharacter == "Wakamo")
                wakamoBuyPanel.SetActive(false);
        }
    }

    private IEnumerator StartCountdownCycle(int numberOfCycles)
    {
        for (int i = 0; i < numberOfCycles; i++)
        {
            yield return StartCoroutine(Countdown(20)); // 첫 카운트다운
            isBuyPanelInteractable = false;
            canFire = true;

            BuyphasePannel.SetActive(false);

            if (LockinManager.lastSelectedCharacter == "AL-1S")
                al1SBuyPanel.SetActive(false);
            else if (LockinManager.lastSelectedCharacter == "Wakamo")
                wakamoBuyPanel.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            canGoAttack = true;

            yield return StartCoroutine(Countdown(100)); // 두 번째 카운트다운

            BuyphasePannel.SetActive(true);
            isBuyPanelInteractable = true;
        }
    }

    private IEnumerator Countdown(int duration)
    {
        int timeLeft = duration;

        UpCountdownText.color = Color.black;

        while (timeLeft >= 0)
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;
            UpCountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (duration == 100 && timeLeft <= 20)
            {
                UpCountdownText.color = Color.red;
            }

            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        UpCountdownText.color = Color.black;

        if (_hp > 0)
        {
            winAnimation.SetActive(true);
            winAnimation.GetComponent<Animation>().Play();
            StartCoroutine(DisableWinAnimationAfterDelay(winAnimation.GetComponent<Animation>().clip.length));
        }
    }

    private IEnumerator DisableWinAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winAnimation.SetActive(false);
    }
}
