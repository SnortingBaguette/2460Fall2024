using UnityEngine;

public class RotatePlayerModel : MonoBehaviour
{

    private Vector3 turnRight = new Vector3(1.6f, 1.6f, 1.6f);
    private Vector3 turnLeft = new Vector3(1.6f, 1.6f, -1.6f);


    public void RotateRight()
    {
        transform.localScale = turnRight;
    }

    public void RotateLeft()
    {
        transform.localScale = turnLeft;
    }
}
