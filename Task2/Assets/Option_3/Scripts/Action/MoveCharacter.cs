using UnityEngine;


public class MoveCharacter : Character, ISprint
{
    [SerializeField] private int _speed;
    [SerializeField] private int _speedSprint;
    private bool _isWalkToBack;
    public int Speed { get => _speed; set => _speed = value; }
    public int SpeedSprint { get => _speedSprint; set => _speedSprint = value; }
    public bool IsSprinte { get; set; }

    public enum TravelOption
    {
        Walk = 0,
        Run = 1
    }

    [SerializeField] private TravelOption _travelOption;
    protected override void Awake()
    {
        base.Awake();
        InputActions.Option_3.Sprint.started += cst => SpeedCharacter();
        InputActions.Option_3.Sprint.started += cst => ActiveSprint();
    }

    protected override void Start()
    {
        SpeedSprint = 10;
        Speed = 5;
    }
    public void Move()
    {
        if (JumpCharacter.IsGround)
        {
            if (_travelOption == TravelOption.Walk)
                Walk();
            else
                Sprint();
        }
        else
        {
            Animator.SetBool("IsMove", false);
            Animator.SetFloat("SpeedRun", 0);
        }

    }

    private void ActiveSprint()
    {
        IsSprinte = !IsSprinte;
        _travelOption = TravelOption.Run;
    }
    public void Sprint()
    {
        if (IsSprinte && InputActions.Option_3.Move.ReadValue<Vector2>().y == 0)
        {
            var movement = Vector3.forward;
            AddForceToPlayer(CalculationTarget(movement), Vector3.forward.z);
        }
        else
        {
            IsSprinte = false;
            _travelOption = TravelOption.Walk;
        }
    }

    private void Walk()
    {
        var y = InputActions.Option_3.Move.ReadValue<Vector2>().y;
        var movement = new Vector3(0, 0, y);
        if (y < 0)
        {
            Physics.gravity = Vector3.down * 150f;
            _isWalkToBack = true;
        }
        else
        {
            Physics.gravity = Vector3.down * 9.81f;
            _isWalkToBack = false;
        }
        if (movement.magnitude > 0.001f)
        {
            AddForceToPlayer(CalculationTarget(movement), y);
        }
        else
        {
            Animator.SetBool("IsMove", false);
        }
    }
    private Vector3 CalculationTarget(Vector3 movement)
    {
        movement = Camera.main.transform.TransformDirection(movement);
        Vector3 currentVelocity = Rb.velocity;
        Vector3 targetVelocity = movement * SpeedCharacter();
        Vector3 velocityTarget = (targetVelocity - currentVelocity);
        return velocityTarget;
    }
    private void AddForceToPlayer(Vector3 velocityTarget, float direction)
    {
        Rb.AddForce(velocityTarget, ForceMode.VelocityChange);
        Animator.SetBool("IsMove", true);
        Animator.SetFloat("SpeedRun", direction * SpeedCharacter());
    }
    private int SpeedCharacter() => InputActions.Option_3.Sprint.inProgress && !_isWalkToBack || IsSprinte && !_isWalkToBack ? SpeedSprint : Speed;
    public void Rotate()
    {
        transform.Rotate(0, RotateSpeed(), 0);
    }
    private float RotateSpeed() => InputActions.Option_3.Rotate.ReadValue<Vector2>().x <= 0.1f && InputActions.Option_3.Rotate.ReadValue<Vector2>().x >= -0.1f ? 0 : InputActions.Option_3.Rotate.ReadValue<Vector2>().x;

    protected override void OnDestroy()
    {
        InputActions.Option_3.Sprint.started -= cst => ActiveSprint();
        InputActions.Option_3.Sprint.started -= cst => SpeedCharacter();
    }
}
