using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Analytics;

public class RaidA : MonoBehaviour
{
    [SerializeField] private GridBoss _gridboss;
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Setting")]
    [SerializeField] private int _howmouch;
    [SerializeField] private int _width, _height;
    public bool Raidnow = false;

    private void Start()
    {
       // gen(_howmouch);
       StartCoroutine(_event());
    }
    public void gen(int count)
    {
        for (int i = 0 ; i < count; i++)
        {
            Genone();
        }
    }

    public bool Genone()
    {
        List<Tile> _emptyTiles = new List<Tile>();
        for (int x = 0 ; x < _width ; x++)
        {
            for(int y = 0 ; y < _height ; y ++)
            {
                Tile tile = _gridboss.GetTile(x,y);
                if(tile != null && !tile.isOccupied)
                {
                    _emptyTiles.Add(tile);
                }

            }
        }
        if (_emptyTiles.Count == 0)
        {
            Debug.LogWarning("no free bro");
            return false;
        }

        Tile _targetTile = _emptyTiles[Random.Range(0,_emptyTiles.Count)];

        GameObject _pickprefabs = _enemyPrefabs[Random.Range(0,_enemyPrefabs.Length)];

        Vector3 _genpos = new Vector3(_targetTile.transform.position.x , _targetTile.transform.position.y , 0f);
        GameObject _genre = Instantiate(_pickprefabs, _genpos , Quaternion.identity);
        //_targetTile.setOccupied();
        //_targetTile.setwalkable(false);

        return true;
    }

    void Update()
    {
        if(Raidnow)
        {
           gen(_howmouch); 
           Raidnow = false;
           StartCoroutine(_event());
        }
        
    }

    public IEnumerator _event()
    {
        yield return new WaitForSeconds(30f);

        Raidnow = true;
        Debug.Log("Raid!");
    }
}
