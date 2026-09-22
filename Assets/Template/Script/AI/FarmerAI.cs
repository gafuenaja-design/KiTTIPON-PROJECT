using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAI : MonoBehaviour
{
    //[SerializeField] private float _speed;
    [SerializeField] private float damage;

    private FarmJob _currentJob;
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
            if(!_currentJob._farm._havestable)
            {
                _currentJob = null;
                return;
            }
            gowork();
            
        }
    }

    void findJob()
    {
        //Debug.Log("Finding Job");
        FarmJob[] _farmlist = FindObjectsOfType<FarmJob>();

        FarmJob closestJob = null;
        float closestDistance = Mathf.Infinity;
        
        

        

         foreach (FarmJob job in _farmlist)
         {
           if (job == null) continue;

           if(!job.cantake() || !job._farm._havestable) continue;
           
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
        //Debug.Log("Going to Job");
        
        MovePosition.setmoveposition(_currentJob.transform.position);

        float _distance = Vector3.Distance(transform.position, _currentJob.transform.position);

        if(_distance < 2f)
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
            if(_currentJob._HP <= 0)
            {
                _currentJob.harvestdone();
            }
        }
        _currentJob = null;
    }
    
}
