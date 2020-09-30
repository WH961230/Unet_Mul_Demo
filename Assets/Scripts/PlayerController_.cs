using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerController_ : MonoBehaviour
{
    private Camera p_camera;

    public float moveSensitive = 30f;
    public float lookSensitive = 80f;

    public float jumpSpeed = 2f;//跳跃速度

    public float gravity = 9.8f;//重力

    //位移矢量
    Vector3 moveDir = Vector3.zero;
    //旋转矢量
    Vector3 rot = Vector3.zero;
    //角色控制器
    private CharacterController p_charactorController;
    //检测是否跳跃
    private bool p_Jump = false;



    void Start()
    {
        p_camera = GetComponentInChildren<Camera>();
        p_charactorController = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        //移动
        Move();

        //相机旋转视角
        RotateView();

        //跳跃
        Jump();

        //持续重力

    }



    //移动
    void Move()
    {
        //获取方向水平垂直的变化值
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveDir.x = h;
        moveDir.z = v;
        //移动
        //transform.Translate(moveDir * moveSensitive * Time.deltaTime);

        GetComponent<CharacterController>().Move(moveDir * moveSensitive * Time.deltaTime);
    }



    //相机旋转视角
    void RotateView()
    {
        //水平旋转 垂直旋转
        rot.y += Input.GetAxis("Mouse X") * lookSensitive * Time.deltaTime;
        rot.x = rot.x - Input.GetAxis("Mouse Y") * lookSensitive * Time.deltaTime;

        //控制y轴方向旋转角度
        if(rot.x > 90f)
        {
            rot.x = 90f;
        }
        else if(rot.x < -90f)
        {
            rot.x = -90f;
        }
        //赋值angle角度
        p_camera.transform.localEulerAngles = rot;
    }



    //跳跃
    void Jump()
    {
        Debug.Log("是否在地板上：" + GetComponent<CharacterController>().isGrounded);

        //检测跳跃信号
        if(!p_Jump)
        {
            p_Jump = Input.GetKeyDown(KeyCode.Space);
        }

        //如果在地板上，且跳跃信号为true
        if(p_charactorController.isGrounded)
        {
            if(p_Jump)
            {       
                //上锁
                p_Jump = false;
            }
        }
    }

}
