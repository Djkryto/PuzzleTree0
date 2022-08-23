using UnityEngine;
using TMPro;

public class ReadableItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        var readableItems = FindObjectsOfType<ReadableItem>();
        foreach(var item in readableItems)
        {
            item.Readed += Read;
            item.ClosedText += CloseText;
        }
    }

    private void Read(string text)
    {
        _textContainer.SetActive(true);
        _text.text = text;
    }

    private void CloseText()
    {
        _textContainer.SetActive(false);
    }
}
