using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject TargetObj = null;
    public float Speed = 7.5f;

    private void Update()
    {
        if (TargetObj != null)
        {
            transform.position = Vector3.Lerp(transform.position, TargetObj.transform.position + new Vector3(0.0f, 0.0f, -10.0f), Speed * Time.deltaTime);
        }
    }
}