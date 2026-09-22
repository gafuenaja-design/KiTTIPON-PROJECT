using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    private ItemUI _itemUI;
    private ItemUI itemui
    {
        get
        {
            if(_itemUI == null)
            {
                _itemUI = FindObjectOfType<ItemUI>();

            }
            return _itemUI;
        }
    }


    void Update()
    {
        if(_health <= 0)
        {
            itemui._npc();
            Destroy(gameObject);
        }
    }
    public void _TakeDamage(float damage)
    {
        _health -= damage;
    }

}
