using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    [SerializeField] private Image _blackScreenImage;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    [SerializeField] private WheelExplosion _wheelExplosion;
    [SerializeField] private int _startSceneIndex;
    [SerializeField] private Spawn _spawn;
    [SerializeField] private Car _carStaticPrefab;
    [SerializeField] private GameObject _gameplayUIGroup;
    private float _durationSceneInSeconds = 3f;

    private void Awake()
    {
        _cinemachineInputProvider.enabled = true;
        _wheelExplosion.OnExplosion += () => { StartCoroutine(ScenarioEnd()); };
        Cursor.visible = false;
    }

    private void ShowingScreenImage(float aplha)
    {
        var ratio = aplha == 0f ? -1f : 1f;
        Color newColor = _blackScreenImage.color;
        newColor.a += ratio * 0.01f;
        _blackScreenImage.color = newColor;
    }

    private IEnumerator ScenarioEnd()
    {
        float timer = 0f;
        while (timer < _durationSceneInSeconds)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        while (_blackScreenImage.color.a < 1f)
        {
            ShowingScreenImage(1f);
            yield return null;
        }
        _carStaticPrefab.gameObject.SetActive(true);
        StartCoroutine(ScreenShowing());
    }

    private IEnumerator ScreenShowing()
    {
        gameObject.SetActive(false);
        _spawn.SpawnPlayer();
        while (_blackScreenImage.color.a > 0f)
        {
            ShowingScreenImage(0f);
            yield return null;
        }
    }
}
