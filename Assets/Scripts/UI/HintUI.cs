using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveObjectHint;
    [SerializeField] private TextMeshProUGUI _takeObjectHintText;
    [SerializeField] private TextMeshProUGUI _inspectObjectHintText;
    [SerializeField] private TextMeshProUGUI _learnObjectHintText;
    [SerializeField] private TextMeshProUGUI _readingObjectHintText;
    [SerializeField] private GameObject _portableObjectHint;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Vision.Detected += ShowHint;
        _player.Vision.Undetected += UnshowHint;
    }

    private void UnshowHint()
    {
        _interactiveObjectHint.gameObject.SetActive(false);
        _portableObjectHint.gameObject.SetActive(false);
    }

    private void ShowHint()
    {
        var interactiveItem = _player.Vision.InteractiveItem;

        TryShowHint(interactiveItem.Takeable, _takeObjectHintText.gameObject);
        TryShowHint(interactiveItem.Inspectable, _inspectObjectHintText.gameObject);
        TryShowHint(interactiveItem.Portable, _portableObjectHint.gameObject);
        TryShowHint(interactiveItem.Learn, _learnObjectHintText.gameObject);
        TryShowHint(interactiveItem.Reading, _readingObjectHintText.gameObject);

        if (_inspectObjectHintText.gameObject.activeSelf || _takeObjectHintText.gameObject.activeSelf || _learnObjectHintText.gameObject.activeSelf
            || _readingObjectHintText.gameObject.activeSelf)
            _interactiveObjectHint.gameObject.SetActive(true);
        else
            _interactiveObjectHint.gameObject.SetActive(false);
    }

    private void TryShowHint<T>(T item, GameObject hint)
    {
        if (item != null)
            hint.SetActive(true);
        else
            hint.SetActive(false);
    }
}
