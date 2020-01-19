using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害陷阱
/// </summary>
public class DamageTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 碰撞检测持续扣血
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other) //改用OnTriggerStay检测停留持续掉血
    {
        PlayerController player = other.GetComponent<PlayerController>(); //检测是否是玩家碰撞
        if (player != null)
        {
            player.ChangeHP(-1);
        }

    }
}
