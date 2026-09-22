using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmJob : JobManager
{
   //[SerializeField] private float _mindtime;
   [SerializeField] public float _HP;
   [SerializeField] private ItemUI _itemUI;
   [SerializeField] public Farm _farm;
   private Tile _mytile;

   public bool _gethavest;

   

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

   public void harvestdone()
   {
      _HP = 10;
      _itemUI._addfood(Random.Range(1,2));
      _farm._regrow();
   }


}
