using UnityEngine;

public class JumpCharacter : Character, IJump
{
    [SerializeField] private int _forceJump;

    private static bool _isGround;
    public float ForceJump { get => _forceJump; }
    public static bool IsGround { get => _isGround; set => _isGround = value; }

    protected override void Awake()
    {
        base.Awake();
        InputActions.Option_3.Jump.started += jump => Jumping();
    }

    public void Jumping()
    {
        if (IsGround)
        {
            Animator.SetTrigger("Jump");
            Rb.AddForce(Vector3.up * ForceJump, ForceMode.Impulse);
            Rb.velocity = transform.forward * ForceJump / 100;
            IsGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            IsGround = true;
    }
    protected override void OnDestroy()
    {
        InputActions.Option_3.Jump.started -= jump => Jumping();
    }

}
