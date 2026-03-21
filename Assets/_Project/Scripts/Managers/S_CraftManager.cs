using System.Collections.Specialized;
using System.Data.Common;
using UnityEngine;
using System.Collections.Generic;

public class CraftManager : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    [SerializeField] private float _snapRadius;
    private int _currentTabletIndex = 0;
    const int NUM_TABLETS = 3;

    private int _selectedSymbolId = -1;
    private float _currentSymbolScale = 1f;
    private float _currentRotation = 0f;
    private int _selectedMaterialId = -1;

    private List<PlacedSymbol> PlacedSymbols = new List<PlacedSymbol>();

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
    }

    public void ResizeSymbol(float factor)
    {
        if (_selectedSymbolId < 0) return;

        _currentSymbolScale = Mathf.Clamp(_currentSymbolScale + factor, 0.5f, 2f);
    }

    public void RotateSymbol(float factor)
    {
        if (_selectedSymbolId < 0) return;

        _currentRotation = (_currentRotation + factor) % 360f;
        if (_currentRotation < 0) _currentRotation = 360f - _currentRotation;
    }

    public void UnselectSymbol()
    {
        _selectedSymbolId = -1;
        _currentSymbolScale = 1f;
        _currentRotation = 0f;
    }

    public void PlaceSymbol(Vector2 position)
    {
        if (_selectedSymbolId >= 0 && PlacedSymbols.Count < 128)
            PlacedSymbols.Add(new PlacedSymbol { Id = _selectedSymbolId, Position = position, Scale = _currentSymbolScale });

        UnselectSymbol();
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
}
