using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MilitaryBaseBuild : MonoBehaviour
{
 [SerializeField] private GridBoss _grid;
 [SerializeField] private GameObject _prefabs;
 [SerializeField] private int _Bwidth = 3;
 [SerializeField] private int _Bheight = 3;
 [SerializeField] public bool _militarybaseplace = false;
[SerializeField] private ItemUI itemUI;
 

    void Update()
    {
        if (_militarybaseplace)
        {
            if(Input.GetMouseButtonDown(0))
            {
                place();
            }
        }
    }

    public void setplace()
    {
        _militarybaseplace = true;
    }


 private void place()
    {
        Vector3 _mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int centerx = Mathf.RoundToInt(_mouseworldpos.x);
        int centery = Mathf.RoundToInt(_mouseworldpos.y);

        int startx = centerx - (_Bwidth/2);
        int starty = centery - (_Bheight/2);

        if(_grid.canplacehere(startx, starty, _Bwidth , _Bheight))
        {
            int cost = 5;
            if(!itemUI._usewood(cost))
            {
                Debug.Log("NAHH POOLL");
                _militarybaseplace = false;
                return;
            }

            Vector3 _spawnpos = new Vector3 (centerx,centery);
            itemUI._usewood(50);

             Instantiate(_prefabs, _spawnpos, Quaternion.identity);
            _militarybaseplace = false;


            _grid.setoccupyarea(startx,starty,_Bwidth,_Bheight ,true);
            Debug.Log("done");
        }
        else
        {
            Debug.Log("nah");
            _militarybaseplace = false;
        }
    }

 
}
