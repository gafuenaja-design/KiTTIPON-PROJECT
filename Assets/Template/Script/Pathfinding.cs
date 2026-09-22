using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance;

    [SerializeField] private GridBoss _gridBoss;

    private void Awake()
    {
        Instance = this;

        if (_gridBoss == null)
        {
            _gridBoss = FindObjectOfType<GridBoss>();
        }
    }

    public List<Vector3> FindPath(
        Vector3 startPosition,
        Vector3 endPosition)
    {
        if (_gridBoss == null)
        {
            Debug.LogError("Pathfinding: ไม่พบ GridBoss");
            return null;
        }

        Tile startTile =
            _gridBoss.GetTileFromWorldPosition(startPosition);

        Tile endTile =
            _gridBoss.GetTileFromWorldPosition(endPosition);

        if (startTile == null)
        {
            Debug.LogWarning(
                "Pathfinding: Start อยู่นอก Grid"
            );

            return null;
        }

        if (endTile == null)
        {
            Debug.LogWarning(
                "Pathfinding: End อยู่นอก Grid"
            );

            return null;
        }

        List<Tile> openList =
            new List<Tile>();

        HashSet<Tile> closedList =
            new HashSet<Tile>();

        Dictionary<Tile, int> gCost =
            new Dictionary<Tile, int>();

        Dictionary<Tile, int> hCost =
            new Dictionary<Tile, int>();

        Dictionary<Tile, Tile> cameFrom =
            new Dictionary<Tile, Tile>();

        openList.Add(startTile);

        gCost[startTile] = 0;

        hCost[startTile] =
            GetDistance(
                startTile,
                endTile
            );

        while (openList.Count > 0)
        {
            Tile currentTile =
                GetLowestFCostTile(
                    openList,
                    gCost,
                    hCost
                );

            if (currentTile == endTile)
            {
                return CalculatePath(
                    endTile,
                    cameFrom
                );
            }

            openList.Remove(currentTile);

            closedList.Add(currentTile);

            foreach (Tile neighbour in
                GetNeighbours(currentTile))
            {
                if (neighbour == null)
                {
                    continue;
                }

                if (closedList.Contains(neighbour))
                {
                    continue;
                }

                // occupied เดินผ่านไม่ได้
                // แต่ถ้าเป็นจุดหมาย อนุญาตให้เข้าได้
                if (!neighbour.iswalkable)
                {
                    continue;
                }

                int currentGCost =
                    gCost[currentTile];

                int newGCost =
                    currentGCost +
                    GetDistance(
                        currentTile,
                        neighbour
                    );

                if (!gCost.ContainsKey(neighbour) ||
                    newGCost < gCost[neighbour])
                {
                    gCost[neighbour] =
                        newGCost;

                    hCost[neighbour] =
                        GetDistance(
                            neighbour,
                            endTile
                        );

                    cameFrom[neighbour] =
                        currentTile;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        Debug.LogWarning(
            "Pathfinding: หาเส้นทางไม่ได้"
        );

        return null;
    }

    private List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours =
            new List<Tile>();

        int x =
            Mathf.RoundToInt(
                tile.transform.position.x
            );

        int y =
            Mathf.RoundToInt(
                tile.transform.position.y
            );

        // ขวา
        neighbours.Add(
            _gridBoss.GetTile(
                x + 1,
                y
            )
        );

        // ซ้าย
        neighbours.Add(
            _gridBoss.GetTile(
                x - 1,
                y
            )
        );

        // ขึ้น
        neighbours.Add(
            _gridBoss.GetTile(
                x,
                y + 1
            )
        );

        // ลง
        neighbours.Add(
            _gridBoss.GetTile(
                x,
                y - 1
            )
        );

        return neighbours;
    }

    private int GetDistance(
        Tile a,
        Tile b)
    {
        int xDistance =
            Mathf.Abs(
                Mathf.RoundToInt(
                    a.transform.position.x
                )
                -
                Mathf.RoundToInt(
                    b.transform.position.x
                )
            );

        int yDistance =
            Mathf.Abs(
                Mathf.RoundToInt(
                    a.transform.position.y
                )
                -
                Mathf.RoundToInt(
                    b.transform.position.y
                )
            );

        return xDistance + yDistance;
    }

    private Tile GetLowestFCostTile(
        List<Tile> tileList,
        Dictionary<Tile, int> gCost,
        Dictionary<Tile, int> hCost)
    {
        Tile lowest =
            tileList[0];

        for (int i = 1; i < tileList.Count; i++)
        {
            Tile tile =
                tileList[i];

            int lowestFCost =
                gCost[lowest] +
                hCost[lowest];

            int tileFCost =
                gCost[tile] +
                hCost[tile];

            if (tileFCost < lowestFCost)
            {
                lowest = tile;
            }
        }

        return lowest;
    }

    private List<Vector3> CalculatePath(
        Tile endTile,
        Dictionary<Tile, Tile> cameFrom)
    {
        List<Vector3> path =
            new List<Vector3>();

        Tile currentTile =
            endTile;

        path.Add(
            currentTile.transform.position
        );

        while (cameFrom.ContainsKey(currentTile))
        {
            currentTile =
                cameFrom[currentTile];

            path.Add(
                currentTile.transform.position
            );
        }

        path.Reverse();

        return path;
    }
}