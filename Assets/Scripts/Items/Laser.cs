using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Laser : InteractiveItem, IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private Transform _rayStart;
    [SerializeField] private GameObject _rayVisualization;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _laserDistance;
    private Camera _camera;
    private IRefractable _hitObject;
    private ITakeable _takeable;
    private IEnumerator _laserWorking;
    private bool _laserIsActive = false;

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
        _camera = Camera.main;
    }

    public void Use()
    {
        _audioSource.Play();
        _laserIsActive = !_laserIsActive;
        _rayVisualization.SetActive(_laserIsActive);
        if (_laserIsActive)
            StartCoroutine(_laserWorking);
        else
        {
            StopCoroutine(_laserWorking);
            _hitObject?.ClearRefraction();
        }
    }

    private IEnumerator ActivateLaser()
    {
        Vector3 cameraCenter = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f);
        while(true)
        {
            Ray ray = _camera.ScreenPointToRay(cameraCenter);
            _hitObject?.ClearRefraction();

            if (!Physics.Raycast(ray, out RaycastHit hit, _laserDistance))
            {
                yield return null;
                continue;
            }

            if (hit.collider.TryGetComponent(out _hitObject))
                _hitObject.Refract(hit.point, transform.forward);

            yield return null;
        }
    }


}
