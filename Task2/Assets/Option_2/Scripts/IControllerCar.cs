public interface IControllerCar
{
    public float Force { get;}
    public float MaxAngle { get;}
    public float ForceBrakeTorque { get; set; }
    public void Move();
    public void BrakeTorque();

    public void RotateColliders();

}
