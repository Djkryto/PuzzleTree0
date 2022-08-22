using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [SerializeField] private GameSettings _gameSettings;
    private Dictionary<string, IData> _gameData;
    private bool _isNewGame = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Debug.LogWarning("GameSession is already exist!");
        }
    }

    public void SaveGameData(IData data)
    {
        _gameData[data.DataId] = data;
    }

    public IData LoadGameData(string dataId)
    {
        return _gameData[dataId];
    }
}
