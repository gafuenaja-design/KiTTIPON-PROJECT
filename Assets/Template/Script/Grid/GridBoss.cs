using UnityEngine;

public class GridBoss : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    private Tile[,] _tiles;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tiles = new Tile[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(
                    _tilePrefab,
                    new Vector3(x, y, 1),
                    Quaternion.identity
                );

                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset =
                    (x % 2 == 0 && y % 2 != 0) ||
                    (x % 2 != 0 && y % 2 == 0);

                spawnedTile.Init(isOffset);

                _tiles[x, y] = spawnedTile;
            }
        }

        if (_cam != null)
        {
            _cam.position = new Vector3(
                (float)_width / 2 - 0.5f,
                (float)_height / 2,
                -10f
            );
        }
    }

    public Tile GetTile(int x, int y)
    {
        if (x < 0 || x >= _width ||
            y < 0 || y >= _height)
        {
            return null;
        }

        return _tiles[x, y];
    }

    public Tile GetTileFromWorldPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);

        return GetTile(x, y);
    }

    public int Width => _width;
    public int Height => _height;

    public bool canplacehere(
        int startx,
        int starty,
        int sizex,
        int sizey)
    {
        for (int x = startx; x < startx + sizex; x++)
        {
            for (int y = starty; y < starty + sizey; y++)
            {
                Tile tile = GetTile(x, y);

                if (tile == null || tile.isOccupied)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void setoccupyarea(
        int startx,
        int starty,
        int sizex,
        int sizey,
        bool occupied)
    {
        for (int x = startx; x < startx + sizex; x++)
        {
            for (int y = starty; y < starty + sizey; y++)
            {
                Tile tile = GetTile(x, y);

                if (tile != null)
                {
                    tile.setOccupied(occupied);
                }
            }
        }
    }
    public void setwalkablearea(
    int startx,
    int starty,
    int sizex,
    int sizey,
    bool walkable)
{
    for (int x = startx; x < startx + sizex; x++)
    {
        for (int y = starty; y < starty + sizey; y++)
        {
            Tile tile = GetTile(x, y);

            if (tile != null)
            {
                tile.setwalkable(walkable);
            }
        }
    }
}
}