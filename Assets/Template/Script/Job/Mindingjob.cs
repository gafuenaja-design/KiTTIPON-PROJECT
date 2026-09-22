using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindingjob : JobManager
{
   //[SerializeField] private float _mindtime;
   [SerializeField] public float _HP;
   [SerializeField] private ItemUI _itemUI;
   private Tile _mytile;

    void Start()
    {
        if(_itemUI == null)
      {
         _itemUI = FindObjectOfType<ItemUI>();
      }
      Collider2D[] _hits = Physics2D.OverlapPointAll(transform.position);

      foreach (Collider2D hit in _hits)
      {
         Tile tile = hit.GetComponent<Tile>();
         if (tile != null)
         {
            _mytile = tile;
            break;
         }
      }
    }

    void Update()
   {
      if (_HP <= 0)
      {
         _itemUI._addstone(Random.Range(5,10));
         _mytile.clearOccupied();
         _mytile.setwalkable(true);
         Destroy(gameObject);
      }
   }
}
