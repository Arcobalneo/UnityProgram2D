using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherController : MonoBehaviour
{
    Animator anime;
    bool isAttacking;
    public bool isATK; //攻击判定有效标志

    float attack_cd;
    float attack_timer;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        isAttacking = false;
        isATK = false;
        attack_cd = Random.Range(3, 7); //攻击间隔3-7秒随机
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == false&&attack_timer >= 0) //未发起攻击
        {
            attack_timer -= Time.deltaTime;
            if (attack_timer < 0) isAttacking = true;
        }
        if (isAttacking) {
            Attack();
            attack_timer = attack_cd;
            attack_cd = Random.Range(3, 7);
            Debug.Log("下次攻击需间隔：" + attack_cd + "秒");
            isATK = false;
        }
    }

    void Attack()
    {
        anime.SetTrigger("Attack");
        Invoke("SetATKStatus",1f);
        anime.SetTrigger("Stop");
        isAttacking = false;
    }
    void SetATKStatus() {
        isATK = true;
    }
}
