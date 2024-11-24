using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Vector3 cameraOffset = new Vector3(0f, 1.54f, -10f);
    public GameObject cameraTarget;
    public GameObject player;
    private float zoomX;
    private float zoomY;
    private Vector3 zoom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        zoomX = player.transform.position.x - cameraTarget.transform.position.x;
        zoomY = player.transform.position.y - cameraTarget.transform.position.y;
        zoom.z = (zoomX + zoomY) * .7f;
        if(zoom.z >0f)
        {
            zoom.z *= -1;
        }
        transform.position = cameraTarget.transform.position + cameraOffset + zoom;


    }
}
