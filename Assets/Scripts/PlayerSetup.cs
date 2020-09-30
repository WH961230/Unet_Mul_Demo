using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;

    Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for(int i = 0; i < componentToDisable.Length; i++)
            {
                //禁用非本地玩家制定组件
                componentToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            //禁用场景摄像机
            sceneCamera.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
        {
            //开启场景摄像机
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
