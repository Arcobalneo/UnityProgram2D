using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTrap : MonoBehaviour
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
            akFish.BeChaos();
            Destroy(this.gameObject);
        }
        Debug.Log("混乱海藻");
    }
}
