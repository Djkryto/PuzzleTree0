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
    [SerializeField] private Crystal[] _crystals;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Camera _cameraNight;
    private ITakeable _takeable;
    private bool _laserIsActive = false;
    private IEnumerator _laserWorking;
    public override IPortable Portable => null;
    public override ITakeable Takeable => _takeable;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => this;
    public override IReadable Readable => null;
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
        ActivatePointCrystal();
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
                if (_cameraNight.enabled)
                {
                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.SetPosition(1, _laserLight.transform.position);
                }
            }
            yield return null;
        }
    }

    private void ActivatePointCrystal()
    {
        for(int i = 0; i < _crystals.Length; i++)
        {
            _crystals[i].Clear();
        }
    }
}
