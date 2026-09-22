using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BuilderAI : MonoBehaviour
{
    [SerializeField] private float damage;

    private BuildJob _currentJob;
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
    }

    
    void Update()
    {
        
        if (_currentJob == null && !picked.activeSelf)
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
        BuildJob[] _buildlist = FindObjectsOfType<BuildJob>();

        BuildJob closestJob = null;
        float closestDistance = Mathf.Infinity;
        
        

        

         foreach (BuildJob job in _buildlist)
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
        Collider2D col = _currentJob.GetComponent<Collider2D>();
        Vector2 targetpos = col.ClosestPoint(transform.position);
        MovePosition.setmoveposition(targetpos);

        float _distance = Vector3.Distance(transform.position, targetpos);

        if(_distance < 2.0f)
        {
            dojob();
        }
    }

    void dojob()
    {
        //Debug.Log("Doing Job");
        _currentJob._HP -= damage * Time.deltaTime;

        if (_currentJob._HP <= 0)
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
    
}
