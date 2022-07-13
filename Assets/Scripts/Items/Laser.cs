using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Laser : InteractiveItem, IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private GameObject _laserLight;
    [SerializeField] private Transform _laserSourceTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _laserLine;
    [SerializeField] AudioSource _audioSource;
    private ITakeable _takeable;
    private bool _laserIsActive = false;
    private IEnumerator _laserWorking;

    public override IPortable Portable => throw new System.NotImplementedException();
    public override ITakeable Takeable => _takeable;
    public override IInspectable Inspectable => throw new System.NotImplementedException();
    public override IUseable Useable => this;
    public override ILearn Learn => throw new System.NotImplementedException();
    public override IReading Reading => throw new System.NotImplementedException();
    public ItemSO ItemData => _itemData;

    private void Awake()
    {
        _takeable = new TakeableItem(gameObject, _rigidbody, _itemData);
        _laserWorking = ActivateLaser();
    }

    public void Use()
    {
        _audioSource.Play();
        _laserIsActive = !_laserIsActive;

        if (_laserIsActive)
            StartCoroutine(_laserWorking);
        else
            StopCoroutine(_laserWorking);

        _laserLight.SetActive(_laserIsActive);
        _laserLine.SetActive(_laserIsActive);

    }

    private IEnumerator ActivateLaser()
    {
        while(true)
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _laserLight.transform.position = hit.point;
            }
            yield return null;
        }
    }
}
