using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private GameObject _miner;
    [SerializeField] private GameObject _lumberjack;
    [SerializeField] private GameObject _farmer;
    [SerializeField] private GameObject _builder;
    [SerializeField] private BasePlacer _base;
    [SerializeField] private ItemUI _itemUI;
    

    void Start()
    {
        if(_itemUI == null)
      {
         _itemUI = FindObjectOfType<ItemUI>();
      }
    
    }

    public void BUYminer()
    {

        if(_itemUI==null && _base == null)
        {
            
            _itemUI = FindObjectOfType<ItemUI>();
            _base = FindObjectOfType<BasePlacer>();
        }

        if(_base == null || _base.newbase == null)
        return;

        if(_itemUI != null)
       {

        int cost = 1;

            if(_itemUI._npcamount >= _itemUI._maxnpcamount)
            {
                Debug.Log("full!");
                return;
            }

            if(!_itemUI._usefood(cost))
            {
                //Debug.Log("cant buy miner");
                return;
            }
            
            


        GameObject newminer = Instantiate(_miner,_base.newbase.transform.position + new Vector3(0,2,0),quaternion.identity);
        _itemUI._npc();
       }
       
    }

     public void BUYlumberjack()
    {

        if(_itemUI==null && _base == null)
        {
            
            _itemUI = FindObjectOfType<ItemUI>();
            _base = FindObjectOfType<BasePlacer>();
        }

        if(_base == null || _base.newbase == null)
        return;

        if(_itemUI != null)
       {

        int cost = 1;

            if(_itemUI._npcamount >= _itemUI._maxnpcamount)
            {
                Debug.Log("full!");
                return;
            }
            if(!_itemUI._usefood(cost))
            {
                //Debug.Log("cant buy miner");
                return;
            }
            

        GameObject newlunberlack = Instantiate(_lumberjack,_base.newbase.transform.position + new Vector3(1,2,0),quaternion.identity);
        _itemUI._npc();
       }
       
    }

      public void BUYfarmer()
    {

        if(_itemUI==null && _base == null)
        {
            
            _itemUI = FindObjectOfType<ItemUI>();
            _base = FindObjectOfType<BasePlacer>();
        }

        if(_base == null || _base.newbase == null)
        return;

        if(_itemUI != null)
       {

        int cost = 1;

            if(_itemUI._npcamount >= _itemUI._maxnpcamount)
            {
                Debug.Log("full!");
                return;
            }
            if(!_itemUI._usefood(cost))
            {
                //Debug.Log("cant buy miner");
                return;
            }

        GameObject newfarmer = Instantiate(_farmer,_base.newbase.transform.position + new Vector3(-1,2,0),quaternion.identity);
        _itemUI._npc();
       }
       
    }

     public void BUYbuilder()
    {

        if(_itemUI==null && _base == null)
        {
            
            _itemUI = FindObjectOfType<ItemUI>();
            _base = FindObjectOfType<BasePlacer>();
        }

        if(_base == null || _base.newbase == null)
        return;

        if(_itemUI != null)
       {

        int cost = 1;

            if(_itemUI._npcamount >= _itemUI._maxnpcamount)
            {
                Debug.Log("full!");
                return;
            } 
            if(!_itemUI._usefood(cost))
            {
                //Debug.Log("cant buy miner");
                return;
            }


        GameObject newbuilder = Instantiate(_builder,_base.newbase.transform.position + new Vector3(-1,3,0),quaternion.identity);
        _itemUI._npc();
       }
    }



}
