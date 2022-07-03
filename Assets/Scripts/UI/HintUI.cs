using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveObjectHint;
    [SerializeField] private TextMeshProUGUI _takeObjectHintText;
    [SerializeField] private TextMeshProUGUI _inspectObjectHintText;
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
        TryShowInspectHint(interactiveItem);
        TryShowTakeHint(interactiveItem);
        TryShowPortableHint(interactiveItem);

        if(_inspectObjectHintText.gameObject.activeSelf || _takeObjectHintText.gameObject.activeSelf)
            _interactiveObjectHint.gameObject.SetActive(true);
        else
            _interactiveObjectHint.gameObject.SetActive(false);
    }

    private void TryShowTakeHint(InteractiveItem interactiveItem)
    {
        try
        {
            if (interactiveItem.Inspectable != null)
                _inspectObjectHintText.gameObject.SetActive(true);
        }
        catch
        {
            _inspectObjectHintText.gameObject.SetActive(false);
        }
    }

    private void TryShowInspectHint(InteractiveItem interactiveItem)
    {
        try
        {
            if (interactiveItem.Takeable != null)
                _takeObjectHintText.gameObject.SetActive(true);
            else
                _takeObjectHintText.gameObject.SetActive(false);
        }
        catch
        {
            _takeObjectHintText.gameObject.SetActive(false);
        }
    }

    private void TryShowPortableHint(InteractiveItem interactiveItem)
    {
        try
        {
            if (interactiveItem.Portable != null)
                _portableObjectHint.SetActive(true);
            else
                _portableObjectHint.SetActive(false);
        }
        catch
        {
            _portableObjectHint.SetActive(false);
        }
    }


}
