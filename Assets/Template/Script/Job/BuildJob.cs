using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class BuildJob : JobManager
{
   [SerializeField] public float _HP;
   [SerializeField] private ItemUI _itemUI;
   [SerializeField] private GameObject _building;

   private Tile _mytile;
   private GridBoss gridBoss;

    private void Awake()
    {
      gridBoss = FindObjectOfType<GridBoss>();
    }

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
        Instantiate(_building,transform.position,Quaternion.identity);
        int sizex = 1;
        int sizey = 1;
        BoxCollider2D boxcal = _building.GetComponent<BoxCollider2D>();
        if(boxcal != null)
         {
            sizex = Mathf.RoundToInt(boxcal.size.x);
            sizey = Mathf.RoundToInt(boxcal.size.y);
         }

         if(gridBoss != null && _mytile != null)
         {
            int startx = Mathf.RoundToInt(_mytile.transform.position.x);
            int starty = Mathf.RoundToInt(_mytile.transform.position.y);
            if(sizex >= 3)
            {
               startx -= 1;
            }
            if(sizey >= 3)
            {
               starty -= 1;
            }
            gridBoss.setwalkablearea(startx , starty , sizex , sizey , false);
           // Debug.Log(startx + "," + starty + "," + sizex + "," + sizey);
         }
         else
         {
            //Debug.Log("gridboss == null");
         }


         Destroy(gameObject);
      }
   }
}
