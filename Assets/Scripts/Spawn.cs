using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<GameObject> _enabledObjects;
    [SerializeField] private Player _player;

    public void SpawnPlayer()
    {
        _enabledObjects.ForEach(obj => obj.SetActive(true));
        _player.transform.position = _spawnPoint.position;
        _player.transform.rotation = _spawnPoint.rotation;
    }
}
