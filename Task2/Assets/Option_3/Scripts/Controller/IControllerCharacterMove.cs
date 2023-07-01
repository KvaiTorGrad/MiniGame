
public interface IControlleblCharacter
{
    public abstract IMovement Movement { get; set; }
    public abstract ISprint Spreent { get; set; }
    public abstract IJump Jump { get; set; }
    public abstract IShoot Shoot { get; set; }
}
public interface IMovement
{
    public int Speed { get;set; }
    public void Move();
    public void Rotate();
}
public interface ISprint : IMovement
{
    public int SpeedSprint { get; set; }
    public bool IsSprinte { get; set; }
    public void Sprint();
}
public interface IJump
{
    public float ForceJump { get; }
    public static bool IsGround { get; set; }
    public void Jumping();
}

public interface IShoot
{
    public bool WhisWeapon { get; set; }
    public void Fire();
    public void ActionWeapon();
}