using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBuild : MonoBehaviour
{
    [SerializeField] private float _health;
    private GridBoss _gridBoss;
    private Tile _mytile;
    private GameObject _building;

    void Awake()
    {
        _gridBoss = FindObjectOfType<GridBoss>();
    }

    void Start()
    {
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
      _building = gameObject;
    }

    void Update()
    {
        if(_health <= 0)
        {
            if(gameObject.CompareTag("Main"))
            {
                Debug.Log("Game Over");
            }
            int sizex = 1;
            int sizey = 1;
            BoxCollider2D boxcal = _building.GetComponent<BoxCollider2D>();
            if(boxcal != null)
         {
            sizex = Mathf.RoundToInt(boxcal.size.x);
            sizey = Mathf.RoundToInt(boxcal.size.y);
         }
            if(_gridBoss != null && _mytile != null)
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
            _gridBoss.setoccupyarea(startx, starty, sizex ,sizey, false);
            Destroy(gameObject);

         }
        }
    }
    public void _TakeDamage(float damage)
    {
        _health -= damage;
    }
}
