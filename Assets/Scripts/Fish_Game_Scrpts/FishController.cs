using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    float speed = 5f;
    private Vector2 lookDirection = new Vector2(1, 0); //默认朝向右
    bool isEating;

    //角色耐力
    float maxHP = 100;
    float curHP;

    Animator anime;
    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        isEating = false;

        curHP = 70;
        UIManager.instance.UpdateHPbar(curHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        Eat();
        move();
    }

    void move() {
        float moveX = Input.GetAxisRaw("Horizontal"); //A:-1 D:1 NoK:0
        float moveY = Input.GetAxisRaw("Vertical"); //W:1 S:-1 NoK:0
        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0  ) //没有按键时朝向不变moveVector.y != 0
        {
            lookDirection = moveVector; 
        }
        anime.SetFloat("LookX", lookDirection.x * -1f);

        if (moveX != 0 || moveY != 0)
        {
            Vector2 newposition = rbody.position;
            newposition += moveVector * speed * Time.deltaTime;
            rbody.MovePosition(newposition);
            //changeHP(-0.1f);
        }

        
    }

    void Eat() {
        if (Input.GetKeyDown(KeyCode.J) && isEating == false) //按J切换为吃模式
        {
            isEating = true;
            anime.SetTrigger("Acting");
            Debug.Log("吃！");
        }
        if (Input.GetKeyDown(KeyCode.K) && isEating == true) //按K切换出吃模式
        {
            isEating = false;
            anime.SetTrigger("Stop");
            Debug.Log("崩撤卖 溜！");
        }
    }

    public void changeHP(float addHP) {
        curHP = Mathf.Clamp(curHP + addHP,0,maxHP);
        UIManager.instance.UpdateHPbar(curHP, maxHP); //更新耐力UI
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FisherController fishHook = other.GetComponent<FisherController>();
        if(fishHook != null)
        {
            if (fishHook.isATK == true) //鱼钩攻击有效判定
            {
                Debug.Log("逮到！");
                changeHP(-10);
                //TODO:1.被击中后无敌时间，防止被一次攻击多次击中缪撒 2.击中判定的打磨，靠flag存在问题
            }           
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        FisherController fishHook = other.GetComponent<FisherController>();
        if (fishHook != null)
        {
            if (isEating == true && fishHook.isATK == false)
            {
                Debug.Log("吃到乐");
                changeHP(0.2f);
            }
        }
    }
}
