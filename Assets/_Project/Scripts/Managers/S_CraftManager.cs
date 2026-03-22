using System.Collections.Specialized;
using System.Data.Common;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CraftManager : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    [SerializeField] private float _snapRadius;
    [SerializeField] private FollowCursor _symbolPrevisu;
    [SerializeField] private Slider _scaleSlider;
    [SerializeField] private Slider _rotateSlider;
    [SerializeField] private Transform _baseTexture;
    [SerializeField] private List<AudioClip> _audioClipList;
    [SerializeField] private List<GameObject> _tabletList;
    [SerializeField] private float _minimumRotation;
    [SerializeField] private float _maximumRotation;

    private float _firstSizeX;

    private int _currentTabletIndex = 0;
    private int _previousTabletIndex;
    const int NUM_TABLETS = 3;

    private int _selectedSymbolId = -1;
    private int _selectedMaterialId = -1;

    [NonSerialized] public Sprite _selectedMaterial;

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
        _firstSizeX = _baseTexture.GetComponent<RectTransform>().sizeDelta.x / 2;
    }

    public void NextTablet()
    {
        _previousTabletIndex = _currentTabletIndex;
        _currentTabletIndex++;
        if (_currentTabletIndex > NUM_TABLETS - 1) _currentTabletIndex = 0;
        UpdateTablet();
    }

    public void PreviousTablet()
    {
        _previousTabletIndex = _currentTabletIndex;
        _currentTabletIndex--;
        if (_currentTabletIndex < 0) _currentTabletIndex = NUM_TABLETS - 1;

        UpdateTablet();
    }

    private void UpdateTablet()
    {
        _tabletList[_previousTabletIndex].gameObject.SetActive(false);
        _tabletList[_currentTabletIndex].gameObject.SetActive(true);
    }

    public void SelectSymbol(int id)
    {
        UnselectMaterial();
        if (id == _selectedSymbolId)
        {
            UnselectSymbol();
            return;
        }
        _selectedSymbolId = id;
        _symbolPrevisu.ChangeSymbol(SymbolManager.Instance.GetSymbolById(id)._symbolPrefab);
    }

    public void SelectMaterial(Sprite sprite)
    {
        UnselectSymbol();
        _selectedMaterial = sprite;
    }

    private void UnselectMaterial()
    {
        _selectedMaterial = null;
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
        _symbolPrevisu.ResetSymbol();
    }

    public void PlaceSymbol(Vector2 position, Vector3 scale, Quaternion rotation)
    {
        if (_selectedSymbolId >= 0 && PlacedSymbols.Count < 128)
        {
            GameObject go_symbol = Instantiate(SymbolManager.Instance.GetSymbolById(_selectedSymbolId)._symbolPrefab, position, rotation, _baseTexture);
            go_symbol.transform.localScale = scale;
            PlacedSymbols.Add(new PlacedSymbol { Id = _selectedSymbolId, Position = position, Scale = _scaleSlider.value });
            S_SFXManager.Instance.PlayRandomClip(_audioClipList, transform, 1.0f);
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
        _baseTexture.position = new Vector2(Mathf.Clamp(_baseTexture.position.x + position, _minimumRotation, _maximumRotation), _baseTexture.position.y);
    }

    public List<PlacedSymbol> GetPlacedSymbols()
    {
        return PlacedSymbols;
    }
}
