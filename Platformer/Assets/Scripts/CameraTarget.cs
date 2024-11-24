using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public GameObject player;
    private Rigidbody playerRb;
    private Vector3 offset;
    private float translationSpeed = 5f;
    public Vector3 var;

    public void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();   
    }
    private void FixedUpdate()
    {
        offset = playerRb.velocity * 0.15f;
        var = Vector3.Lerp(transform.position, player.transform.position + offset, translationSpeed * Time.deltaTime);
        //transform.position = player.transform.position + offset;
        transform.position = var;
    }
}
