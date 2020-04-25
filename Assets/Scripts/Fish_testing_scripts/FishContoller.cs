using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishContoller : MonoBehaviour
{
    float stepTime = 0.7f;
    GameObject MsgSender;
    public Vector3 moveDirection;
    Rigidbody2D rbody;

    //移动相关
    bool stopOnce;
    bool nextMove;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        MsgSender = GameObject.Find("Enemy");
        nextMove = true;
        stopOnce = true;
    }

    private void FixedUpdate()
    {
        if (nextMove == true && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ) )
        {
            nextMove = false;
            fishMove();           
        }
        if (stopOnce == true && Input.GetKeyDown(KeyCode.Space))
        {
            stopOnce = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == targetPos)
        {
            targetPos = Vector3.zero; //防止反复发送消息
            nextMove = true; //控制一次移动结束前不能中途移动
            MsgSender.SendMessage("EnemyMove","如果你追到我"); //小鱼移动结束后让安康鱼移动
        }

        if (stopOnce == false)
        {
            MsgSender.SendMessage("EnemyMove", "如果你追到我");
            stopOnce = true;
        }
    }

    void fishMove()
    {
        Vector3 curPos = this.transform.position;
        float moveX = Input.GetAxisRaw("Horizontal"); // 控制水平移动方向 A:-1 D:1 NoK:0
        float moveY = Input.GetAxisRaw("Vertical"); // 控制垂直移动方向 W:1 S:-1 NoK:0
        moveDirection = new Vector3(moveX, moveY, 0);
        targetPos = curPos + moveDirection;
        transform.DOMove(targetPos , stepTime);
        
    }

    
}
