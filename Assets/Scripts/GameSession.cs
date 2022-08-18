using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [SerializeField] private GameData _gameData;
    [SerializeField] private PlayerData _playerData;
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
        Instance.LoadGameData();
    }

    public void SaveData()
    {
        var saveData = FindObjectsOfType<Saver>();
        foreach(var data in saveData)
        {
            data.Save();
        }
    }

    public void LoadGameData()
    {
        var saveData = FindObjectsOfType<Saver>();
        foreach(var data in saveData)
        {
            data.Load();
        }
    }
   
}
