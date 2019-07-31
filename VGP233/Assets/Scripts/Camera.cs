using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject mPlayer;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - mPlayer.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = mPlayer.transform.position + offset;
    }
}
