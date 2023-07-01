using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RotateCrystal : MonoBehaviour
{
    [SerializeField] private float _speed;

    private delegate void RotateObj();
    private RotateObj _rotateObj;
    private Animator _animator;
    public enum RotationOption
    {
        Animation = 0,
        TransformRotate = 1,
    }
    public RotationOption _rotationOption;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _speed = 100;
    }

    void Update()
    {
        InitOption();
        _rotateObj();
    }
    private void InitOption()
    {
        if (_rotationOption == RotationOption.Animation)
            _rotateObj = AnimatorRotate;
        else
            _rotateObj = TransformRotate;
    }
    private void TransformRotate()
    {
        transform.Rotate(Vector3.up * _speed * Time.deltaTime);
        _animator.enabled = false;
    }
    private void AnimatorRotate()
    {
        _animator.enabled = true;
    }
}
