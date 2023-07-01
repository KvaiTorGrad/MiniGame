using System;
using UnityEngine;

public class Car : MonoBehaviour, IControllerCar
{
    [SerializeField] private CarWeel[] carWeel;
    [SerializeField] private float _force;
    [SerializeField] private float _maxAngle;
    private float _forceBrakeTorque;

    public float Force { get => _force; }
    public float MaxAngle { get => _maxAngle; }
    public float ForceBrakeTorque { get => _forceBrakeTorque; set => _forceBrakeTorque = value; }

    private Controls _inputActions;
    private void Awake()
    {
        _inputActions = new Controls();
        _inputActions.Option_2.Stoping.started += cst => ForceBrake();
        _inputActions.Enable();
    }

    private void Start()
    {
        ForceBrakeTorque = 5000f;
    }
    private void Update()
    {
        SynchWheelWithCollider();
    }

    private void SynchWheelWithCollider()
    {
        for (int i = 0; i < carWeel.Length; ++i)
            RotateWheel(carWeel[i].ColliderWheels, carWeel[i].Wheels);
    }

    private void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        transform.rotation = rotation;
        transform.position = position;
    }

    public void Move()
    {
        var vertical = _inputActions.Option_2.MoveAndRotate.ReadValue<Vector2>().y;
        carWeel[0].ColliderWheels.motorTorque = vertical * Force;
        carWeel[1].ColliderWheels.motorTorque = vertical * Force;
    }

    public void BrakeTorque()
    {
        for (int i = 0; i < carWeel.Length; i++)
            carWeel[i].ColliderWheels.brakeTorque = ForceBrake();
    }
    private float ForceBrake() => _inputActions.Option_2.Stoping.inProgress ? ForceBrakeTorque : 0f;
    public void RotateColliders()
    {
        var horizontal = _inputActions.Option_2.MoveAndRotate.ReadValue<Vector2>().x;
        carWeel[0].ColliderWheels.steerAngle = horizontal * MaxAngle;
        carWeel[1].ColliderWheels.steerAngle = horizontal * MaxAngle;
    }
    private void OnDestroy()
    {
        _inputActions.Option_2.Stoping.started -= cst => ForceBrake();
    }
}

[Serializable]
public struct CarWeel
{
    [SerializeField] private Transform _wheels;
    public Transform Wheels { get => _wheels; }
    [SerializeField] private WheelCollider _colliderWheels;
    public WheelCollider ColliderWheels { get => _colliderWheels; }
}
