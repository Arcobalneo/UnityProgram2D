using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制子弹移动，碰撞
/// </summary>
public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Awake() //使用start无法在刚生成时获取到刚体发生报错，使用awake替代
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,2f); //两秒后摧毁
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 子弹移动
    /// </summary>
    public void Move(Vector2 moveDirection,float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    /// <summary>
    /// 跟敌人碰撞消失
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if(ec != null)
        {
            Debug.Log("击中乐");
            ec.Fixed();
        }
        Destroy(this.gameObject);
    }
}
