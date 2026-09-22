using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;

    [SerializeField] public SpriteRenderer _renderer;

    public bool isOccupied { get; private set; }
    public bool iswalkable { get; private set; }

    public void Init(bool isOffset)
    {
        if (_renderer != null)
        {
            _renderer.color =
                isOffset ? _offsetColor : _baseColor;
        }

        isOccupied = false;
        iswalkable = true;
    }

    public void setOccupied(bool value)
    {
        isOccupied = value;
    }

    public void clearOccupied()
    {
        isOccupied = false;
    }


    public void setwalkable(bool value)
    {
        iswalkable = value;
    }
}