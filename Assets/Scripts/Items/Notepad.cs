using UnityEngine;

public class Notepad : InteractiveItem, IReadable
{
    [SerializeField] private GameObject _textCanvas;
    [SerializeField] private AudioSource _soundList;
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

    public void ReadText()
    {
        ShowText(true);
    }

    public void CloseText()
    {
        ShowText(false);
    }

    private void ShowText(bool state)
    {
        _isRead = state;
        _textCanvas.SetActive(_isRead);
        _soundList.Play();
    }
}
