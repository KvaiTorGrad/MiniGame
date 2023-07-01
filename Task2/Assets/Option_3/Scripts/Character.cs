using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour, IControlleblCharacter
{
    private Controls _inputActions;
    private Rigidbody _rb;
    private Animator _animator;

    public Controls InputActions { get => _inputActions; }
    public Rigidbody Rb { get => _rb; }
    public Animator Animator { get => _animator; }
    public IMovement Movement { get; set; }
    public ISprint Spreent { get; set; }
    public IJump Jump { get; set; }
    public IShoot Shoot { get; set; }

    protected virtual void Awake()
    {
        Movement = GetComponent<IMovement>();
        Jump = GetComponent<IJump>();
        Shoot = GetComponent<IShoot>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputActions = new Controls();
        _inputActions.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    protected virtual void Start() { }
    protected virtual void OnDestroy() { }
}
