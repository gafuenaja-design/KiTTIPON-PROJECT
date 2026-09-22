using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour , IMoveVelocity
{
    //[SerializeField] private float _speed;
   private Vector3 _velocityVector;
   private Rigidbody2D _rigidbody2D;
   //private Character_Base character_Base

   private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void setvelocity(Vector3 velocityVector)
    {
        this._velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _velocityVector;
    }
}
