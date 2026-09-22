using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LumberjackAI : MonoBehaviour
{

    [SerializeField] private float damage;

    private WoodJob _currentJob;
    private GridBoss _gridBoss;
    private IMovePosition _movePosition;
    public GameObject picked;

   
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

    void Awake()
    {
        _currentJob = null;
        _gridBoss = FindObjectOfType<GridBoss>();
    }
    void Update()
    {
        if (_currentJob == null&& !picked.activeSelf)
        {
            findJob();
            return;  

        }
        if(picked.activeSelf)
        {
            finisjjob();
        }

        if(_currentJob != null)
        {
            gowork();
        }
    }

    void findJob()
    {
        //Debug.Log("Finding Job");
        WoodJob[] _woodlist = FindObjectsOfType<WoodJob>();

        WoodJob closestJob = null;
        float closestDistance = Mathf.Infinity;
        
        

        

         foreach (WoodJob job in _woodlist)
         {
           if (job == null) continue;

           if(!job.cantake()) continue;
           
            float distance = Vector3.Distance(transform.position, job.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestJob = job;
            }
        }
        _currentJob = closestJob;

        if(_currentJob != null)
        {
            _currentJob.takejob();
        }
        
        
    }

    void gowork()
    {
        Tile _jobtile = _gridBoss.GetTileFromWorldPosition(_currentJob.transform.position);
        Tile _targettile = findwalkablearound(_jobtile);
        if(_targettile == null)
        {
            Debug.Log("no space around tree");
            return;
        }
        MovePosition.setmoveposition(_targettile.transform.position);

        float _distance = Vector3.Distance(transform.position, _currentJob.transform.position);

        if(_distance < 2f)
        {
            dojob();
        }
    }

    void dojob()
    {
        //Debug.Log("Doing Job");
        _currentJob._HPtree -= damage * Time.deltaTime;

        if (_currentJob._HPtree <= 0)
        {
            finisjjob();
        }
    }

    void finisjjob()
    {
        //Debug.Log("Finishing Job");
        if (_currentJob != null)
        {
            _currentJob.finishjob();
        }
        _currentJob = null;

    }
    
    Tile findwalkablearound(Tile center)
    {
        int x = Mathf.RoundToInt(center.transform.position.x);
        int y = Mathf.RoundToInt(center.transform.position.y);

        Tile[] tiles =
        {
            _gridBoss.GetTile(x + 1, y),
            _gridBoss.GetTile(x - 1, y),
            _gridBoss.GetTile(x, y + 1),
            _gridBoss.GetTile(x, y - 1)
        };

        Tile _closesttile = null;
        float _closestdis = Mathf.Infinity;
        foreach(Tile tile in tiles)
        {
            if(tile == null)
            continue;
            if(!tile.iswalkable)
            continue;
            float _dis = Vector3.Distance(transform.position,tile.transform.position);

            if (_dis < _closestdis)
            {
                _closestdis = _dis;
                _closesttile = tile;
            }
        }
        return _closesttile;
    }

    
}
