using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int x;
    public int y;

    public bool _iswalkable;

    public int gcost;
    public int hcost;
    public int fcost;

    public PathNode cameformnode;
    public PathNode(int x,int y,bool _iswalkable)
    {
        this.x = x;
        this.y = y;
        this._iswalkable = _iswalkable;
    }
    public void calculatefcast()
    {
        fcost = gcost + hcost;
    }
}
