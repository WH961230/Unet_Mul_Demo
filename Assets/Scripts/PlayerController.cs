using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float lookSensity = 30f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * _xMov;
        Vector3 movVertical = transform.forward * _zMov;

        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed ;

        motor.Move(_velocity);


        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensity;
        //视角旋转
        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensity;

        Debug.Log(_cameraRotationX + " $$$");

        //cam 视角旋转
        motor.RotateCamera(_cameraRotationX);
    }
}
