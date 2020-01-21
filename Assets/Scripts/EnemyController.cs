using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人控制
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float speed = 3f; //初始移动速度

    public float changeDirectionTime = 2f; //改变运动方向
    private float changeDirectionTimer; 
    public bool isVertical; //是否垂直移动
    private bool isFixed; //是否被击中
    private Vector2 moveDirection; //移动方向
    private Animator anime;
    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeDirectionTimer = changeDirectionTime;
        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return; //如果处于被击中修复状态 不执行其他动作

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            moveDirection *= -1; //改为反方向移动
            changeDirectionTimer = changeDirectionTime;
        }

        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        anime.SetFloat("moveX",moveDirection.x);
        anime.SetFloat("moveY",moveDirection.y);
    }

    /// <summary>
    /// 刚体检测给予玩家碰撞伤害
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>(); //检测是否为玩家碰撞到

        if (player != null)
        {
            Debug.Log("kick your ass!");
            player.ChangeHP(-1);
        }
    }

    /// <summary>
    /// 敌人被子弹击中
    /// </summary>
    public void Fixed()
    {
        isFixed = true;
        rbody.simulated = false; //被击中后禁用物理效果
        anime.SetTrigger("fixed"); //播放被修复动画
    }
}
