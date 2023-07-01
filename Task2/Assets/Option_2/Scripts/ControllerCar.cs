using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCar : MonoBehaviour
{
    private IControllerCar _controllerCar;
    void Start()
    {
        _controllerCar = GameObject.FindGameObjectWithTag("Car").GetComponent<IControllerCar>();
    }
    void FixedUpdate()
    {
        _controllerCar.Move();
        _controllerCar.BrakeTorque();
        _controllerCar.RotateColliders();
    }
}
