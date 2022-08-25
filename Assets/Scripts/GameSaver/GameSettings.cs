using UnityEngine;

[System.Serializable]
internal class GameSettings : ScriptableObject
{
    public bool IsNewGame = true;

    [Header("Sound settings")]
    public float SoundEffectVolume;
    public float MusicVolume;
    [Header("Graphic settings")]
    public int ScreenHeight;
    public int ScreenWidth;
}
