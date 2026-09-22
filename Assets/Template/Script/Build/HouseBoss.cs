using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBoss : MonoBehaviour
{
    [SerializeField] ItemUI itemUI;

    public static int _housenow;
    private int _old = 0;
    void Update()
    {
        int house = GameObject.FindGameObjectsWithTag("house").Length;
        _housenow = house;
        if(_old < _housenow)//up
        {
           _old = _housenow;
           itemUI._addmaxnpc(10);

        }

        if(_old > _housenow)//down
        {
            _old = _housenow;
            itemUI._addmaxnpc(-10);
            
        }

    }
}
