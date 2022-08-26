using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create SceneSO")]
public class SceneSO : ScriptableObject
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private GameObject _scenePrefab;

    public int Index => _index;
    public string Name => _name;
    public GameObject ScenePrefab => _scenePrefab;
}
