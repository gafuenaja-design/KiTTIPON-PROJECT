using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirectEnemy : MonoBehaviour , IMovePosition
{
    [SerializeField] private float _speed;
   private Vector3 moveposition;
   private EnemyAI _enemyAI;


   private void Awake()
    {
        moveposition = transform.position;
        
    }

    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }

    public void setmoveposition(Vector3 moveposition)
    {
        this.moveposition = moveposition;
    }

    private void Update()
    {
        Vector3 moveDIr = (moveposition - transform.position).normalized;
        if (Vector3.Distance(moveposition, transform.position) < 0.1f ) moveDIr = Vector3.zero;
        GetComponent<IMoveVelocity>().setvelocity(moveDIr * _speed);
        if(_enemyAI != null && _enemyAI.onwar) return;
        if(moveDIr != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDIr.y, moveDIr.x) * Mathf.Rad2Deg + -90f;
            transform.rotation = Quaternion.Euler(0, 0, angle); 
        }
        GetComponent<IMoveVelocity>().setvelocity(moveDIr * _speed);

    }
}
