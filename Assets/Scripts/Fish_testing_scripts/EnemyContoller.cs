using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnemyContoller : MonoBehaviour
{
    
    bool beginEat ;
    GameObject myFood;

    //移动相关
    float stepTime = 0.5f;

    //陷阱相关
    int CoiledStep = 0;
    int ChaosStep = 0;
    int ChaosDir = 1;
    int FoodingStep = 0;
    // Start is called before the first frame update
    void Start()
    {
        beginEat = false;
        myFood = GameObject.Find("PlayerFish");
    }

    // Update is called once per frame
    void Update()
    {
        if (beginEat)
        {

            SecondStep();
            if (FoodingStep == 0) Invoke("SecondStep", stepTime + 0.2f); //+0.2f防止第一步没走完 BUG:beingCoiled条件无法限制陷阱后第二步                               
            beginEat = false;
            if (ChaosDir == -1 && ChaosStep == 0) ChaosDir = 1; //重置混乱状态
        }
    }

    void EnemyMove(string message)
    {
        if (CoiledStep == 0) beginEat = true; //未被缠绕
        else CoiledStep--;
        if (FoodingStep != 0) FoodingStep--;
        if (ChaosStep != 0) ChaosStep--;
    }

    void FirstStep()
    {
        Vector3 curPos = this.transform.position;
        Vector3 moveDirection = myFood.GetComponent<FishContoller>().moveDirection; //第一步根据小鱼移动方向移动
        Vector3 targetPos = curPos + moveDirection;
        transform.DOMove(targetPos, stepTime);       
    }

    void SecondStep()
    {
        Vector3 curFishPos = myFood.transform.position;
        Vector3 myPos = this.transform.position;
        Vector3 objVec = curFishPos - myPos;            
        if (Mathf.Abs(objVec.x) > Mathf.Abs(objVec.y))
        {
            objVec.x = objVec.x / Mathf.Abs(objVec.x) * ChaosDir;
            objVec.y = 0;
        }
        else
        {
            objVec.y = objVec.y / Mathf.Abs(objVec.y) * ChaosDir;
            objVec.x = 0;
        }
        Debug.Log("当前移动向量" + objVec);      
        transform.DOMove((myPos + objVec),stepTime);
        
    }

    /// <summary>
    /// 触发触手缠绕
    /// </summary>
    public void BeCoiled()
    {
        Debug.Log("这波是触手缠绕");
        CoiledStep = 2; //停止两次
    }

    /// <summary>
    /// 触发混乱海藻
    /// </summary>
    public void BeChaos()
    {
        Debug.Log("这波是混乱海藻");
        ChaosDir= -1; //反向
        ChaosStep = 3; //混乱三次
    }

    /// <summary>
    /// 触发鱼饵陷阱
    /// </summary>
    public void BeFooded()
    {
        Debug.Log("这波是肉蛋葱鸡");
        FoodingStep = 4; //缓步三次
    }
}
