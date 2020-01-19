using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可拾取物品被玩家碰撞时检测
/// </summary>
public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //碰撞检测
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            Debug.Log("ruby get strayberry!");
            if (pc.playerCurrentHP < pc.playerMaxHP)
            {
                pc.ChangeHP(1);
                Destroy(this.gameObject);
            }
            
            
        }
    }
}
