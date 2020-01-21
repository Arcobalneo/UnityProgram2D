﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI管理
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } //??单例模式

    public Image HPbar; //角色血条

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curHP"></param>
    /// <param name="maxHP"></param>
    public void UpdateHPbar(int curHP,int maxHP)
    {
        HPbar.fillAmount = (float)curHP / (float)maxHP;
    }
}