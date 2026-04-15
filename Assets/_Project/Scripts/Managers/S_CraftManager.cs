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
    public Transform _baseTexture;
    [SerializeField] private List<AudioClip> _audioClipList;
    [SerializeField] private List<GameObject> _tabletList;
    public float _minimumRotation;
    public float _maximumRotation;

    [Header("Onboarding")]
    [SerializeField] private List<GameObject> _onboardingPanelList;

    private float _firstSizeX;

    private int _currentTabletIndex = 0;
    private int _previousTabletIndex;
    const int NUM_TABLETS = 3;

    private int _selectedSymbolId = -1;
    private int _selectedMaterialId = -1;

    const int NUM_ONBOARDING = 4;
    int _onboardingCurrentIndex = 0;

    [Header("Materials")]
    [NonSerialized] public Sprite _selectedMaterial;
    [SerializeField] private Sprite[] _materialList;
    [SerializeField] private Texture2D[] _materialCursors;

    private List<PlacedSymbol> PlacedSymbols = new List<PlacedSymbol>();
    private bool _waitedOnceAt12;

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

    public void ResetItem()
    {
        PlacedSymbols.Clear();
        
        for (int i = 0; i < _baseTexture.childCount; i++)
        {
            Destroy(_baseTexture.GetChild(i).gameObject);
        }
    }

    public void NextTablet()
    {
        _previousTabletIndex = _currentTabletIndex;
        _currentTabletIndex++;
        if (_currentTabletIndex > NUM_TABLETS - 1) _currentTabletIndex = 0;
        UpdateTablet();

        CheckOnboarding(6);
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
        UnselectSymbol();
        _selectedSymbolId = id;
        _symbolPrevisu.ChangeSymbol(SymbolManager.Instance.GetSymbolById(id)._symbolPrefab);

        CheckOnboarding(3);
        CheckOnboarding(7);
    }

    public void CheckOnboarding(int index)
    {
        Debug.Log("Onboarding : " + _onboardingCurrentIndex);
        Debug.Log("Index send : " + index);

        if (_onboardingCurrentIndex >= _onboardingPanelList.Count)
        {
            _onboardingPanelList[_onboardingCurrentIndex - 1].SetActive(false);
            return;
        }

        if (_onboardingCurrentIndex == 12)
        {
            if (_waitedOnceAt12 == false)
            {
                _waitedOnceAt12 = true;
                return;
            }

            _waitedOnceAt12 = false;
        }

        if (index != _onboardingCurrentIndex) return;

        if (index > 0 && index < _onboardingPanelList.Count)
        {
            if (_onboardingPanelList[index - 1] != null)
                _onboardingPanelList[index - 1].SetActive(false);
        }

        if (_onboardingPanelList[index] != null)
            _onboardingPanelList[index].SetActive(true);

        

        _onboardingCurrentIndex++;

    }

    public void SelectMaterial(int id)
    {
        if (_selectedMaterial == _materialList[id])
        {
            UnselectMaterial();
            return;
        }
        UnselectSymbol();
        UnselectMaterial();
        _selectedMaterial = _materialList[id];

        CheckOnboarding(10);
    }

    public void ResizeSymbol()
    {
        _symbolPrevisu.ChangeScale(_scaleSlider.value * 10);
        CheckOnboarding(8);
    }

    public void RotateSymbol()
    {
        _symbolPrevisu.ChangeRotation(_rotateSlider.value * 360);
    }

    private void UnselectMaterial()
    {
        _selectedMaterial = null;
        _symbolPrevisu.ResetMaterial();
    }

    public void UnselectSymbol()
    {
        ResetCursor();
        _selectedSymbolId = -1;
        _scaleSlider.value = 0.1f;
        _rotateSlider.value = 0f;
        _symbolPrevisu.ResetSymbol();
    }

    public void ChangeCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void PlaceSymbol(Vector2 position, Vector3 scale, Quaternion rotation)
    {
        if (_selectedSymbolId >= 0 && PlacedSymbols.Count < 128)
        {
            GameObject go_symbol = Instantiate(SymbolManager.Instance.GetSymbolById(_selectedSymbolId)._symbolPrefab, position, rotation, _baseTexture);
            go_symbol.transform.localScale = scale;
            PlacedSymbols.Add(new PlacedSymbol { Id = _selectedSymbolId, Position = position, Scale = _scaleSlider.value });
            S_SFXManager.Instance.PlayRandomClip(_audioClipList, transform, 1.0f);

            CheckOnboarding(5);
            CheckOnboarding(9);
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
    }

    public List<PlacedSymbol> GetPlacedSymbols()
    {
        return PlacedSymbols;
    }

    public void HandleStateExit(int state)
    {
        switch (state)
        {
            case (int)S_GameStateManager.GameState.ITEMCRAFTING:
                UnselectSymbol();
                UnselectMaterial();
                break;
            case (int)S_GameStateManager.GameState.ITEMDELIVERY:
                ResetItem();
                break;
        }
    }
}
