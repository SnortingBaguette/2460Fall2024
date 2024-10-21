using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speedModifier = 3f;
    private float horizontalInput;
    public CharacterController characterController;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(horizontalInput * speedModifier, 0, 0);
        characterController.Move(move * Time.deltaTime * speedModifier);
    }

    private void FixedUpdate()
    {
        
        
    }

    private void InputMethod()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

}
