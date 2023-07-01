using UnityEngine;

public class WeaponCharacter : Character, IShoot
{
    public bool WhisWeapon { get; set; }
    private bool _onWeapon;
    private int _lightRay;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light _light;
    protected override void Awake()
    {
        base.Awake();
        InputActions.Option_3.WeaponUp.started += cts => ActionWeapon();
        InputActions.Option_3.Shoot.started += shoot => Fire();
        _lightRay = 200;
        _light.range = _lightRay;
    }
    public void Fire()
    {
        if (!WhisWeapon || !InputActions.Option_3.Shoot.inProgress) return;
        Animator.SetTrigger("Fire");
        _audioSource.PlayOneShot(_audioSource.clip, 1f);
        _particleSystem.Play();
        SetColorToPointRayCast();
    }
    private void SetColorToPointRayCast()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _lightRay, LayerMask.GetMask("Monster")))
        {
            var material = hit.collider.transform.GetComponent<Renderer>().material;
            material.color = new Color(RandomColor(), RandomColor(), RandomColor());
        }
    }
    private float RandomColor() => Random.Range(0f, 1f);
    public void ActionWeapon()
    {
        if (InputActions.Option_3.WeaponUp.inProgress)
        {
            _onWeapon = !_onWeapon;
            Animator.SetBool("IsWeapon", _onWeapon);
            WhisWeapon = _onWeapon;
            _light.enabled = _onWeapon;
        }
    }
    protected override void OnDestroy()
    {
        InputActions.Option_3.Shoot.started -= shoot => Fire();
        InputActions.Option_3.WeaponUp.started -= cts => ActionWeapon();
    }
}
