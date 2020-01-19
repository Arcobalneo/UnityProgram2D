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
    private Vector2 moveDirection; //移动方向
    private Animator anime;
    Rigidbody2D Body;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeDirectionTimer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            moveDirection *= -1; //改为反方向移动
            changeDirectionTimer = changeDirectionTime;
        }

        Vector2 position = Body.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        Body.MovePosition(position);
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
}
