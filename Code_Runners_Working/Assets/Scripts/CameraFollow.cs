using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform playerFollow = null;
    [SerializeField]
    Vector3 offset = new Vector3(0,0,-10);  

    private Quaternion camRot;

    // Start is called before the first frame update
    void Start()
    {
        camRot = gameObject.transform.rotation;
    }

    void LateUpdate()
    {
        gameObject.transform.position = playerFollow.position + offset;
        gameObject.transform.rotation = camRot;
    }
}
