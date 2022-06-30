using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveObjectHint;
    [SerializeField] private TextMeshProUGUI _takeObjectHintText;
    [SerializeField] private TextMeshProUGUI _inspectObjectHintText;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Vision.Detected += ShowHint;
        _player.Vision.Undetected += UnshowHint;
    }

    private void UnshowHint()
    {
        _interactiveObjectHint.gameObject.SetActive(false);
    }

    private void ShowHint()
    {
        var interactiveItem = _player.Vision.InteractiveItem;

        _interactiveObjectHint.gameObject.SetActive(true);

        if (interactiveItem.Inspectable != null)
            _inspectObjectHintText.gameObject.SetActive(true);
        else
            _inspectObjectHintText.gameObject.SetActive(false);

        if (interactiveItem.Takeable != null)
            _takeObjectHintText.gameObject.SetActive(true);
        else
            _takeObjectHintText.gameObject.SetActive(false);
    }
}
