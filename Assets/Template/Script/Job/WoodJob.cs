using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodJob : JobManager
{
   
   [SerializeField] public float _HPtree;
   [SerializeField] private ItemUI _itemUI;
   private Tile _mytile;
    private bool _cuted =false;

    void Start()
    {
        if (_itemUI == null)
      {
         _itemUI = FindObjectOfType<ItemUI>();

      }
      FindTile();
     
    }

    void FindTile()
   {
        Collider2D[] _hits = Physics2D.OverlapPointAll(transform.position);

       foreach(Collider2D hit in _hits)
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
      if (_HPtree <= 0 && !_cuted)
      {
         cut();
      }
   }

   public void cut()
   {
      if (_cuted ) return;
      if (_HPtree <= 0)
      {
         if( _mytile == null) FindTile();
         _cuted = true;
         _itemUI._addwood(Random.Range(5,10));
         _mytile.clearOccupied();
         _mytile.setwalkable(true);
         Destroy(gameObject);
      }
   }
}
