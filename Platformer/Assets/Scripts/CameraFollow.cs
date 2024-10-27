using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerTransform;
    public Vector3 cameraOffset = new Vector3(0f, 1.54f, -10f);
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = playerTransform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.transform.position + cameraOffset/* + playerRb.velocity.normalized*/;
    }
}
