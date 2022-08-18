using UnityEngine;

internal class GameData : ScriptableObject
{
    [SerializeField] private bool _isNewGame = true;

    [Header("Sound settings")]
    [SerializeField] private float _soundEffectVolume;
    [SerializeField] private float _musicVolume;
    [Header("Graphic settings")]
    [SerializeField] private int _screenHeight;
    [SerializeField] private int _screenWidth;
}
