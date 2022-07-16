using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Laser : InteractiveItem, IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private GameObject _laserLight;
    [SerializeField] private Transform _laserSourceTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LineRenderer _laserLine;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private Crystal[] _crystals;
    private ITakeable _takeable;
    private bool _laserIsActive = false;
    private IEnumerator _laserWorking;
    public override IPortable Portable => null;
    public override ITakeable Takeable => _takeable;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => this;
    public override ILearn Learn => null;
    public override IReading Reading => null;
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
        _laserLine.enabled = _laserIsActive;

    }

    private IEnumerator ActivateLaser()
    {
        while(true)
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _laserLine.SetPosition(0, _laserSourceTransform.position);
                _laserLight.transform.position = hit.point;
                _laserLine.SetPosition(1, hit.point);
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
