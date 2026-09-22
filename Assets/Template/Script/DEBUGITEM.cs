using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGITEM : MonoBehaviour
{
    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private bool _DeBug = false;

private void Update()
    {
        if(_DeBug)
        {
            _itemUI._addfood(100);
            _itemUI._addwood(100);
            _itemUI._addstone(100);
            _DeBug = false;
        }
    }
}
