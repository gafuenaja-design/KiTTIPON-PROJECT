using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect :
    MonoBehaviour,
    IMovePosition
{
    [SerializeField] private float _speed = 5f;

    private Vector3 moveposition;

    private EnemyAI _enemyAI;
    private SoldierAI _soldierAI;

    private List<Vector3> _path;
    private int _pathIndex;

    private IMoveVelocity _moveVelocity;

    private void Awake()
    {
        moveposition =
            transform.position;

        _moveVelocity =
            GetComponent<IMoveVelocity>();

        _enemyAI =
            GetComponent<EnemyAI>();

        _soldierAI =
            GetComponent<SoldierAI>();

        if (_moveVelocity == null)
        {
            Debug.LogError(
                gameObject.name +
                " ไม่มี IMoveVelocity"
            );
        }
    }

    public void setmoveposition(
        Vector3 moveposition)
    {
        this.moveposition =
            moveposition;

        if (Pathfinding.Instance == null)
        {
            Debug.LogWarning(
                "ไม่มี Pathfinding.Instance"
            );

            return;
        }

        _path =
            Pathfinding.Instance.FindPath(
                transform.position,
                moveposition
            );

        // จุด 0 คือ Tile ที่เราเริ่มอยู่
        _pathIndex = 1;

        if (_path == null ||
            _path.Count == 0)
        {
           // Debug.LogWarning(
              //  gameObject.name +
                // หา Path ไม่เจอ"
            //);
        }
        else
        {
            //Debug.Log(
                //gameObject.name +
                //" ได้ Path " +
                //_path.Count +
                //" จุด"
            //);
        }
    }

    private void Update()
    {
        if (_moveVelocity == null)
        {
            return;
        }

        if (_path == null ||
            _path.Count == 0)
        {
            _moveVelocity.setvelocity(
                Vector3.zero
            );

            return;
        }

        if (_pathIndex >= _path.Count)
        {
            _moveVelocity.setvelocity(
                Vector3.zero
            );

            return;
        }

        // ถ้ากำลังต่อสู้
       // if// (_soldierAI != null &&
            //_soldierAI.onwar)
       // {
           // _moveVelocity.setvelocity(
              //  Vector3.zero
           // );

           // return;
       // }

        //if (_enemyAI != null &&
           // _enemyAI.onwar)
        //{
            //_moveVelocity.setvelocity(
                //Vector3.zero
           // );

            //return;
        //}

        Vector3 targetPosition =
            _path[_pathIndex];

        // ไม่เอา Z ของ Tile มาทำให้เกิดปัญหา
        targetPosition.z =
            transform.position.z;

        Vector3 moveDir =
            targetPosition -
            transform.position;

        moveDir.z = 0f;

        float distance =
            moveDir.magnitude;

        // ถึง Tile นี้แล้ว → ไป Tile ต่อไป
        if (distance < 0.1f)
        {
            _pathIndex++;

            _moveVelocity.setvelocity(
                Vector3.zero
            );

            return;
        }

        moveDir.Normalize();

        _moveVelocity.setvelocity(
            moveDir * _speed
        );

        // หันตามทิศทางการเดิน
        if (moveDir != Vector3.zero)
        {
            float angle =
                Mathf.Atan2(
                    moveDir.y,
                    moveDir.x
                ) * Mathf.Rad2Deg - 90f;

            transform.rotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    angle
                );
        }
    }
}