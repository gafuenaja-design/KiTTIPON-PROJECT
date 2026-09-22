using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ATKAI : MonoBehaviour
{
    private StatsBuild _statsBuild;
    private GameObject main;
    private IMovePosition _movePosition;
    private float _savedis = 15;
    private EnemyAI _enemyAI;
    private bool _near = false;
    private float _random;
    private float _total;
    
    private IMovePosition MovePosition
    {
        get
        {
            if(_movePosition == null)
            {
                _movePosition = GetComponent<IMovePosition>();

            }
            return _movePosition;
        }
    }
    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }
    void Update()
    {
        Check();
        
        if(_enemyAI._target == null)
        {
            if(_near == false)
            {
                Gotomain();
            }
            
        }


    }
    private void Check()
    {
        if(main == null)
        {
            main = GameObject.FindGameObjectWithTag("Main");
        }

        if(main != null)
        {float dis = Vector3.Distance(transform.position , main.transform.position);
            if(dis <= 10f)
            {
                _near = true;
                
            }
            else if(dis >= 10f)
            {
                _near = false;
            }
        }
    }

    void Gotomain()
    {
        
        if(main == null)
        {
            main = GameObject.FindGameObjectWithTag("Main");
        }
        DoRandom();
        if(main != null)
        {
            MovePosition.setmoveposition(main.transform.position - _total * (main.transform.position - transform.position).normalized);
            
        }
    }
    void DoRandom()
    {
        _random = Random.Range(1,10);
        _total = _savedis + _random;
    }

}
