using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    [SerializeField] private float _attackrange = 20f;
    [SerializeField] private float _seerange = 25f;
    [SerializeField] private float _attackdamage;
    [SerializeField] private float _misschance = 15;
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _firepoint;
    [SerializeField] private float _fireRaya;
    [SerializeField] private GameObject _bulletTrailPrefab;
    [SerializeField] private LayerMask _shootableLayer;

    private bool _cooldown = false;
    private GameObject _target;
    public bool onwar = false;
    private Quaternion _originalRotation;

    private void Awake()
    {
        _originalRotation = _gun.transform.localRotation;

        newrandom();
    }
    void Update()
    {

        if(_target == null)
        {
            findtarget();
            if(_target == null)
            {
                onwar = false;
                _gun.transform.localRotation = _originalRotation;
                return;
            }
            float _distance = Vector3.Distance(transform.position, _target.transform.position);
            if(_distance > _seerange)
            {
                _target = null;
                onwar = false;
                _gun.transform.localRotation = _originalRotation;
                return;
            }


        }
        if(_target != null)
        {
            onwar = true;
             aim();
            if(Vector3.Distance(transform.position, _target.transform.position) <= _attackrange)
            {
                if(!_cooldown)
                {
                    Shoot();
                    StartCoroutine(cooldown());
                }
                
                
            }
        }

        if(_target != null && Vector3.Distance(transform.position, _target.transform.position) >= _seerange)
        {
           _target = null;
          onwar = false;
        }
        if(onwar == true)
        {
             Vector2 _direction = (_target.transform.position - transform.position).normalized;

        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + -90f;
        _gun.transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
        if(onwar == false)
        {
            _gun.transform.localRotation = _originalRotation;
        }

    }

    private void findtarget()
    {
        GameObject[] _enemylist = GameObject.FindGameObjectsWithTag("Enemy");
        float _closestDistance = _seerange;
        GameObject _closestEnemy = null;
        foreach(GameObject enemy in _enemylist)
        {
            float _distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(_distance <= _closestDistance)
            {
                _closestDistance = _distance;
                _closestEnemy = enemy;
            }
        }
        _target = _closestEnemy;

    }

    private void aim()
    {
        Vector2 _direction = (_target.transform.position - transform.position).normalized;

        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + -90f;
        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }

    private void Shoot()
    {
        //Debug.Log("Shooting at " + _target.name);
        float _spread = Random.Range(-_misschance, _misschance);
        Vector2 _spreadDirection = Quaternion.Euler(0, 0, _spread) * _firepoint.up;
        Quaternion _spreadRotation = Quaternion.Euler(0,0,_spread)* transform.rotation;

        var hit = Physics2D.Raycast(_firepoint.position, _spreadDirection, _fireRaya, _shootableLayer);


        var trail = Instantiate(_bulletTrailPrefab, _firepoint.position ,  _spreadRotation);

        var trialScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trialScript.settargetposition(hit.point);
                if(hit.collider.TryGetComponent(out Stats stats))
                {
                    stats._TakeDamage(_attackdamage);
                }
                //Debug.Log("Hit " + hit.collider.name);

            }
        else
        {
            var __endPosition = (Vector2)_firepoint.position + (_spreadDirection * _fireRaya);
            trialScript.settargetposition(__endPosition);
        }
    }

    private void newrandom()
    {
        _attackdamage = Random.Range(20, 30);
        _fireRaya = Random.Range(15, 20);
    }

    IEnumerator cooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(1.0f);
        _cooldown = false;
        newrandom();
    }


}