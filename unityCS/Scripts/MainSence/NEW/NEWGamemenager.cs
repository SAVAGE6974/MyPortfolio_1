using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NEWGamemenager : MonoBehaviour
{
    public NEWSampleLaserClick AL_1S_laserClick;
    
    public Text cooltime_AL_1S_eskill;
    public Text AL_1S_mainTextE;
    public Text AL_1S_mainTextC;
    public Text AL_1S_mainTextQ;
    public Text AL_1S_mainTextX;
    
    public static bool AL_1S_canUseSkillE;
    public static float cooldownTime = 20f;
    public static float currentCooldown = 0f;
    public static bool AL_1S_shootSkillX = false;

    public GameObject AL1Sgun;
    public GameObject Wakamogun;

    private void Awake()
    {
        AL1Sgun.SetActive(false);
        Wakamogun.SetActive(false);
        AL_1S_canUseSkillE = true;
    }

    private void Start()
    {
        // 총기 세팅
        if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL1Sgun.SetActive(true);
            Wakamogun.SetActive(false);

            AL_1S_mainTextC.color = Color.gray;
            AL_1S_mainTextQ.color = Color.gray;
            AL_1S_mainTextX.color = Color.gray;
        }
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            AL1Sgun.SetActive(false);
            Wakamogun.SetActive(true);
        }
    }

    private void Update()
    {
        AL_1S_CheckCooldownE();
        AL1S_SkillC();
        AL1S_SkillQ();
        AL1S_SkillX();
    }

    public void AL1S_SkillC()
    {
        if (Input.GetKeyDown(KeyCode.C) &&
            AL_1S_mainTextC.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S" &&
            NEWUI._hp <= 50)
        {
            AL_1S_mainTextC.color = Color.gray;
            NEWUI._hp += 20;

            if (NEWUI.instance != null)
            {
                NEWUI.instance.UpdateHP();
            }
        }
    }

    public void AL1S_SkillQ()
    {
        if (Input.GetKeyDown(KeyCode.Q) &&
            AL_1S_mainTextQ.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL_1S_mainTextQ.color = Color.gray;

            NEWSampleLaserClick.instance.AL1S_use_Q();
        }
    }
    
    

    public void AL1S_SkillX()
    {
        if (Input.GetKeyDown(KeyCode.X) &&
            AL_1S_mainTextX.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL_1S_mainTextX.color = Color.gray;
            AL_1S_shootSkillX = true;
            AL_1S_laserClick.FireLaserX();
        }
    }

    private void AL_1S_CheckCooldownE()
    {
        if (!AL_1S_canUseSkillE)
        {
            currentCooldown -= Time.deltaTime;
            cooltime_AL_1S_eskill.text = Mathf.Ceil(currentCooldown).ToString() + "s";
            AL_1S_mainTextE.color = Color.gray;

            if (currentCooldown <= 0f)
            {
                AL_1S_canUseSkillE = true;
                cooltime_AL_1S_eskill.text = "";
                AL_1S_mainTextE.color = Color.white;
            }
        }
        else
        {
            AL_1S_mainTextE.color = Color.white;
        }
    }

    public static void AL1S_skillE()
    {
        AL_1S_canUseSkillE = false;
        currentCooldown = cooldownTime;
        Debug.Log("E 키 사용! 20초 쿨타임 시작!");

        if (NEWSampleLaserClick.instance != null)
        {
            NEWSampleLaserClick.fireE();
        }
        else
        {
            Debug.LogWarning("NEWSampleLaserClick.instance is null when using Skill E");
        }
    }
}
