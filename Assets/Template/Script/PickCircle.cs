using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCircle : MonoBehaviour
{
    [SerializeField] private Transform _npc;

    private void LateUpdate()
    {
        transform.position = _npc.position-new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.identity;
    }
}
