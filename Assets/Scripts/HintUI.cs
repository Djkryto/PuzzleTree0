using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveObjectHint;
    [SerializeField] private TextMeshProUGUI _takeObjectHintText;
    [SerializeField] private TextMeshProUGUI _inspectObjectHintText;
    [SerializeField] private Player _player;

    void Start()
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
        var inspectableObject = _player.Vision.InspectableObject;
        var takeableObject = _player.Vision.TakeableObject;

        _interactiveObjectHint.gameObject.SetActive(true);

        if (inspectableObject != null)
            _inspectObjectHintText.gameObject.SetActive(true);
        else
            _inspectObjectHintText.gameObject.SetActive(false);

        if (takeableObject != null)
            _takeObjectHintText.gameObject.SetActive(true);
        else
            _takeObjectHintText.gameObject.SetActive(false);
    }
}
