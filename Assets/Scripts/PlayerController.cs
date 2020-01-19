using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制ruby移动,生命值变化，动作动画等
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;//初始移动速度
    private Vector2 lookDirection = new Vector2(1,0); //默认朝向右
    


    private int maxHP = 5;
    private int currentHP;
    public int playerMaxHP { get { return maxHP; } }
    public int playerCurrentHP { get { return currentHP; } }

    private float invincibleTime = 2f; //无敌时间
    private float invincibleTimer; //无敌计时器
    private bool isVincible;

    Rigidbody2D rubyBody;
    Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        rubyBody = GetComponent<Rigidbody2D>(); //获取人物刚体组件
        anime = GetComponent<Animator>();
        currentHP = 2;
        isVincible = false;
        invincibleTimer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //**受伤无敌计时
        if (isVincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0) isVincible = false; //无敌时间后解除
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // 控制水平移动方向 A:-1 D:1 NoK:0
        float moveY = Input.GetAxisRaw("Vertical"); // 控制垂直移动方向 W:1 S:-1 NoK:0

        Vector2 moveVector = new Vector2(moveX,moveY);
        if (moveVector.x != 0 || moveVector.y != 0) //没有按键时不变
        {
            lookDirection = moveVector;
        }
        anime.SetFloat("Look X",lookDirection.x);
        anime.SetFloat("Look Y",lookDirection.y);

        anime.SetFloat("Speed",moveVector.magnitude);


        Vector2 newposition = rubyBody.position;
        newposition += moveVector * speed * Time.deltaTime;
        rubyBody.MovePosition(newposition); //使用该方法获取位置解决碰撞抖动问题
        
    }

    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="addHP"></param>
    public void ChangeHP(int addHP)
    {
        if (addHP < 0) //玩家受到伤害后
        {
            if (isVincible == true) return; //处于无敌状态不改变生命值
            else
            {
                isVincible = true;
                invincibleTimer = invincibleTime;
            }
        }

        Debug.Log("Before change HP:" + currentHP + "/" + maxHP);
        currentHP = Mathf.Clamp(currentHP+addHP , 0 , maxHP); //约束生命值范围
        Debug.Log("after change HP:" + currentHP + "/" + maxHP);
    }

    
    

   
}
