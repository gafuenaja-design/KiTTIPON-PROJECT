using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasePlacer : MonoBehaviour
{
    [SerializeField] private GridBoss _grid;
    [SerializeField] private GameObject _basePrefab;

    [SerializeField] private GameObject _startminerNPCPrefab;
    [SerializeField] private GameObject _startlumberjackNPCPrefab;
    [SerializeField] private GameObject _startfarmerNPCPrefab;
    [SerializeField] private GameObject _startBuilderNPCPrefab;
    [SerializeField] private ItemUI itemUI;
    public GameObject newbase;
    private RaidA _gen;



    private Camera _cam; 
    private bool _isPlacing = true;
     void Start()
    {
        _cam = Camera.main;
        _gen = FindObjectOfType<RaidA>();
    }

    void Update()
    {
        if (!_isPlacing)
        return;
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBase();
        }
            
        
    }

    void PlaceBase()
    {
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(Input.mousePosition);

        int x = Mathf.RoundToInt(mouseWorldPos.x);
        int y = Mathf.RoundToInt(mouseWorldPos.y);

        Tile centerTile = _grid.GetTile(x, y);
        if (centerTile == null)
        return;

        newbase = Instantiate(_basePrefab, new Vector3 (x,y,0), Quaternion.identity); 

        GameObject newminernpc = Instantiate(_startminerNPCPrefab,newbase.transform.position + new Vector3 (0,2,0), quaternion.identity);
        GameObject newlumberjacknpc = Instantiate(_startlumberjackNPCPrefab,newbase.transform.position + new Vector3 (1,2,0), quaternion.identity);
        GameObject newfarmernpc = Instantiate(_startfarmerNPCPrefab,newbase.transform.position + new Vector3 (-1,2,0), quaternion.identity);
        GameObject newbuildernpc = Instantiate(_startBuilderNPCPrefab,newbase.transform.position + new Vector3 (-1,3,0), quaternion.identity);

        _isPlacing = false;
        itemUI._npc();
        itemUI._addmaxnpc(10);
        _gen._event();
        Debug.Log("Game Start");

    }

}
