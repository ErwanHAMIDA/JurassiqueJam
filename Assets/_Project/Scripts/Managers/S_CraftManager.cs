using System.Collections.Specialized;
using System.Data.Common;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    [SerializeField] private float _snapRadius;
    [SerializeField] private FollowCursor _symbolPrevisu;
    [SerializeField] private Slider _scaleSlider;
    [SerializeField] private Slider _rotateSlider;
    [SerializeField] private Transform _baseTexture;
    [SerializeField] private Transform _secondTexture;

    private float _firstSizeX;

    private int _currentTabletIndex = 0;
    const int NUM_TABLETS = 3;

    private int _selectedSymbolId = -1;
    private int _selectedMaterialId = -1;

    private List<PlacedSymbol> PlacedSymbols = new List<PlacedSymbol>();

    public static CraftManager Instance { get; private set; }

    private void Awake() 
    {         
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        _firstSizeX = _baseTexture.GetComponent<Renderer>().bounds.size.x / 2;
    }

    public void NextTablet()
    {
        _currentTabletIndex = (_currentTabletIndex + 1) % NUM_TABLETS;
    }

    public void PreviousTablet()
    {
        _currentTabletIndex--;
        if (_currentTabletIndex < 0) _currentTabletIndex = NUM_TABLETS - 1;
    }

    public void SelectSymbol(int id)
    {
        _selectedSymbolId = id;
        _symbolPrevisu.ChangeSymbol(SymbolManager.Instance.GetSymbolById(id)._symbolPrefab);
    }

    public void ResizeSymbol()
    {
        _symbolPrevisu.ChangeScale(_scaleSlider.value * 10);
    }

    public void RotateSymbol()
    {
        _symbolPrevisu.ChangeRotation(_rotateSlider.value * 360);
    }

    public void UnselectSymbol()
    {
        _selectedSymbolId = -1;
        _scaleSlider.value = 0.1f;
        _rotateSlider.value = 0f;
    }

    public void PlaceSymbol(Vector2 position, Vector3 scale, Quaternion rotation)
    {
        if (_selectedSymbolId >= 0 && PlacedSymbols.Count < 128)
        {
            GameObject go_symbol = Instantiate(SymbolManager.Instance.GetSymbolById(_selectedSymbolId)._symbolPrefab, position, rotation, _baseTexture);
            go_symbol.transform.localScale = scale;
            PlacedSymbols.Add(new PlacedSymbol { Id = _selectedSymbolId, Position = position, Scale = _scaleSlider.value });
        }
    }

    public void PlaceMaterial(Vector2 position)
    {
        float minDist = float.MaxValue;
        PlacedSymbol closest = null;
        foreach (var zone in PlacedSymbols)
        {
            float dist = Vector2.Distance(position, zone.Position);
            if (dist < minDist && dist <= _snapRadius)
            {
                minDist = dist;
                closest = zone;
            }
        }
        closest.MaterialId = _selectedMaterialId;
    }

    public void MoveBaseTexture(float position)
    {
        _baseTexture.position = new Vector2(_baseTexture.position.x + position, _baseTexture.position.y);
        _secondTexture.position = new Vector2(_baseTexture.position.x + position + _firstSizeX, _baseTexture.position.y);
    }
}
