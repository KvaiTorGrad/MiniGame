using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDecal : MonoBehaviour
{
    [SerializeField] private GameObject _footprint;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform[] _targetPoint;

    private List<GameObject> _prints = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(TimerDestructionPrint());
    }
    public void CreateDecalFootprint(int numberLeg)
    {
        if (JumpCharacter.IsGround)
        {
            var print = Instantiate(_footprint, _targetPoint[numberLeg].position, _targetPoint[numberLeg].rotation);
            print.transform.SetParent(_parent);
            _prints.Add(print);
        }

    }

    private IEnumerator TimerDestructionPrint()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_prints.Count == 0) continue;
            Destroy(_prints[0]);
            _prints.RemoveAt(0);
        }
    }
}
