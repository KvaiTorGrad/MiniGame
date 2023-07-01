using UnityEngine;

public class SetColor : MonoBehaviour
{
    private Controls inputActions;
    private void Awake()
    {
        inputActions = new Controls();
        inputActions.Option_1.Tab.started += ctx => SetColorToClickObject();
        inputActions.Enable();
    }
    private void SetColorToClickObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputActions.Option_1.Position.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Crystal")))
        {
            var renderer = hit.collider.GetComponentInChildren<Renderer>();
            renderer.sharedMaterial.color = new Color(RandomColor(), RandomColor(), RandomColor());
        }
    }

    private float RandomColor() => Random.Range(0f, 1f);
    private void OnDestroy()
    {
        inputActions.Option_1.Tab.started -= ctx => SetColorToClickObject();
    }
}
