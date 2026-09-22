using UnityEngine;
using System.Collections.Generic;

public class DEBUGOCCUPIED : MonoBehaviour
{
    public bool DEBUGMODE;
    private Dictionary<Tile, Color> originalColors = new Dictionary<Tile, Color>();

    void Update()
    {
        // ค้นหาและอัปเดตทุกเฟรม
        Tile[] all = FindObjectsByType<Tile>(FindObjectsSortMode.None);

        foreach (Tile _tile in all)
        {
            if (_tile._renderer != null)
            {
                if (DEBUGMODE)
                {
                    if (!originalColors.ContainsKey(_tile))
                    {
                        originalColors[_tile] = _tile._renderer.color;
                    }
                    if(_tile.isOccupied)
                    {
                        _tile._renderer.color = Color.blue;
                    }
                    else
                    {
                        if (originalColors.ContainsKey(_tile))
                    {
                        _tile._renderer.color = originalColors[_tile];
                    }
                    }
                    
                }
                else
                {
                    if (originalColors.ContainsKey(_tile))
                    {
                        _tile._renderer.color = originalColors[_tile];
                    }
                }
            }
        }
    }
}