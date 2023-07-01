using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{
    private IControlleblCharacter controllerCharacterMove;
    void Start()
    {
        controllerCharacterMove= GameObject.FindGameObjectWithTag("Player").GetComponent<IControlleblCharacter>();
    }

    void FixedUpdate()
    {
        controllerCharacterMove.Movement.Move();
    }
    private void Update()
    {
        controllerCharacterMove.Movement.Rotate();

    }
}
