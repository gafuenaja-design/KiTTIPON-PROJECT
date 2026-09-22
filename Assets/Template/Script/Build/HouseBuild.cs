using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class HouseBuild : MonoBehaviour
{
 [SerializeField] private GridBoss _grid;
 [SerializeField] private GameObject _prefabs;
 [SerializeField] private int _Bwidth = 2;
 [SerializeField] private int _Bheight = 2;
 [SerializeField] public bool _Houseplace = false;
 [SerializeField] private ItemUI itemUI;
 

    void Update()
    {
        if (_Houseplace)
        {
            if(Input.GetMouseButtonDown(0))
            {
                place();
            }
        }
    }

    public void setplace()
    {
        _Houseplace = true;
    }


 private void place()
    {
        Vector3 _mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int startx = Mathf.RoundToInt(_mouseworldpos.x);
        int starty = Mathf.RoundToInt(_mouseworldpos.y);

        if(_grid.canplacehere(startx, starty, _Bwidth , _Bheight))
        {
            int cost = 5;
            if(!itemUI._usestone(cost))
            {
                Debug.Log("NAHH POOLL");
                _Houseplace = false;
                return;
            }

            Vector3 _spawnpos = new Vector3 (startx - 0.5f,starty - 0.5f,0f);
            Instantiate(_prefabs, _spawnpos, Quaternion.identity);
            _Houseplace = false;
            

            _grid.setoccupyarea(startx,starty,_Bwidth,_Bheight, true);
            Debug.Log("done");
        }
        else
        {
            Debug.Log("nah");
            _Houseplace = false;
        }
    }

 
}
