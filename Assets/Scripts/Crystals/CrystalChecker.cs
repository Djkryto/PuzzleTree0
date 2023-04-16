using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalChecker : MonoBehaviour
{
    public Action AllCrystalEnabled;

    [SerializeField] private List<Crystal> _crystals;
    private int _refractionCount;

    private void Start()
    {
        var crystals = FindObjectsOfType<Crystal>();
        _crystals = new List<Crystal>(crystals);

        foreach(var crystal in _crystals)
        {
            //crystal.EnabledCrystal += AddRefraction;
            //crystal.DisabledCrystal += RemoveRefraction;
        }
    }

    private void AddRefraction()
    {
        _refractionCount++;
        if( _refractionCount == _crystals.Count)
        {
            AllCrystalEnabled?.Invoke();
            print("All crystal");
        }
    }

    private void RemoveRefraction()
    {
        _refractionCount--;
    }
}
