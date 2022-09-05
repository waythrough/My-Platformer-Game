using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private GroundChecking groundChecking;

    [SerializeField] private float speed;

    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float timeToPeak = 0.25f;

    [SerializeField] private float jumpGraduation = 0.75f;
    
    private float jumpSpeed;
    private float gravity;

    private float horizontalSpeed;
    private float verticalSpeed;

    private bool jumpRequest;
    private bool jumpGraduationRequest;

    private void Update()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * speed;

        jumpSpeed = ((2 * jumpHeight) / timeToPeak);
        gravity = (-(2 * jumpHeight) / (timeToPeak * timeToPeak));

        if(Input.GetKeyDown(KeyCode.Z) && groundChecking.isGrounded) {
            jumpRequest = true;
        }

        if(Input.GetKeyUp(KeyCode.Z) && verticalSpeed > 0) {
            jumpGraduationRequest = true;
        }

        //Debug.Log("JumpSpeed : " + jumpHeight + ", " + "Gravity :" + gravity);
    }

    private void FixedUpdate()
    {
        if(groundChecking.isGrounded && verticalSpeed <= 0) {
            verticalSpeed = gravity*Time.fixedDeltaTime;
        }

        if (!groundChecking.isGrounded)
        {
            verticalSpeed += gravity*Time.fixedDeltaTime;
        }

        if(jumpRequest && groundChecking.isGrounded) {
            verticalSpeed = jumpSpeed;
            jumpRequest = false;
        }

        if(jumpGraduationRequest) {
            verticalSpeed *= jumpGraduation;
            jumpGraduationRequest = false;
        }

        Vector2 motion = new Vector2(horizontalSpeed, verticalSpeed);

        rigidbody2D.velocity = motion;
    }
}