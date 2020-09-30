using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody rb;

    //当前相机x轴旋转度
    private float currentCameraRotationX = 0f;
    //相机旋转x 变化值
    private float cameraRotationX = 0f;
    //相机旋转限制度数
    private float cameraRotationLimit = 80f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        if(rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        if (cam != null)
        {
            //设定旋转角度
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX,-cameraRotationLimit,cameraRotationLimit);

            //赋值x旋转
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX,0f,0f);
        }
    }

}
