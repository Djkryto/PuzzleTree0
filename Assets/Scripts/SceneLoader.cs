using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private TextMeshProUGUI _loadingPercentage;

    private Animator _animator;
    private AsyncOperation _loadSceneOperation;
    private static bool _shouldPlayOpeningOperation;

    public static void SwitchToScene(string sceneName)
    {
        Instance._animator.SetTrigger("SceneClosing");
        Instance._loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        Instance._loadSceneOperation.allowSceneActivation = false;
    }

    public static void SwitchToScene(int sceneIndex)
    {
        Instance._animator.SetTrigger("SceneClosing");
        Instance._loadSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        Instance._loadSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            _animator = GetComponent<Animator>();
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Debug.LogWarning("SceneLoader is already exists!");
        }

        if (_shouldPlayOpeningOperation)
            Instance._animator.SetTrigger("SceneOpening");
    }

    private void Update()
    {
        if(_loadSceneOperation != null)
        {
            _loadingPercentage.text = Mathf.RoundToInt(_loadSceneOperation.progress * 100) + "%";
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningOperation = true;
        _loadSceneOperation.allowSceneActivation = true;
    }
}
