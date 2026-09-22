using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Analytics;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private GridBoss _gridboss;
    [SerializeField] private GameObject[] _resourcePrefabs;

    [Header("Setting")]
    [SerializeField] private int _howmouch;
    [SerializeField] private int _width, _height;

    private void Start()
    {
        gen(_howmouch);
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

        GameObject _pickprefabs = _resourcePrefabs[Random.Range(0,_resourcePrefabs.Length)];

        Vector3 _genpos = new Vector3(_targetTile.transform.position.x , _targetTile.transform.position.y , 0f);
        GameObject _genre = Instantiate(_pickprefabs, _genpos , Quaternion.identity);
        _targetTile.setOccupied(true);
        _targetTile.setwalkable(false);

        return true;
    }
}
