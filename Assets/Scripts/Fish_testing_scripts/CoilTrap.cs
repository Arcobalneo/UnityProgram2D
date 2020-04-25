using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缠绕陷阱（碰撞之后接下来两次移动停在原地）
/// </summary>
public class CoilTrap : MonoBehaviour
{   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {              
        EnemyContoller akFish = other.GetComponent<EnemyContoller>();
        if (akFish != null)
        {
            akFish.BeCoiled();            
            Destroy(this.gameObject);
        }
        Debug.Log("触手缠绕");
    }

}
