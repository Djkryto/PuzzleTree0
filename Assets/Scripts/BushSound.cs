using UnityEngine;

public class BushSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _bushMoveSound;
    [SerializeField] private AudioClip _bushExitSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            _audioSource.clip = _bushMoveSound;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            if(rigidbody.velocity == Vector3.zero)
            {
                _audioSource.Stop();
            }
            else if(!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _audioSource.Stop();
        _audioSource.clip = _bushExitSound;
        _audioSource.Play();
    }
}
