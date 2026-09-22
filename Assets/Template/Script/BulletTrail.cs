    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _progress;
    [SerializeField] private float _speed = 10f;

    void Start()
    {
        _startPosition = transform.position.WithAxis(Axis.Z, -1);

    }

    void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _progress);

    }
    public void settargetposition(Vector3 targetposition)
    {
        _endPosition = targetposition.WithAxis(Axis.Z, -1);
    }

}
