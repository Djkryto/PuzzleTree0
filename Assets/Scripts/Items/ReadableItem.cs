using System;
using UnityEngine;

public class ReadableItem : InteractiveItem, IReadable
{
    public Action<string> Readed;
    public Action ClosedText;

    [SerializeField] private AudioSource _soundList;
    [TextArea]
    [SerializeField] private string _text;
    private bool _isRead;

    public override IPortable Portable => null;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override IReadable Readable => this;
    public override IUseable Useable => null;

    private void Awake()
    {
        _soundList = GameObject.Find("SoundList").GetComponent<AudioSource>();
    }

    public void SetText(string text)
    {
        _text = text;
    }

    public void ReadText()
    {
        ShowText(true);
        Readed.Invoke(_text);
    }

    public void CloseText()
    {
        ShowText(false);
        ClosedText.Invoke();
    }

    private void ShowText(bool state)
    {
        _isRead = state;
        _soundList.Play();
    }
}
