using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{

    public GameObject Owner;
    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        // set its position to the players plus an offset
        transform.position = Owner.transform.position + Offset;
    }
}
