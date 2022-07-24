using System;
using UnityEngine;

public class WheelExplosion : MonoBehaviour
{
    public Action OnExplosion;

    [SerializeField] private AudioSource _explosionAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out CarControl car))
        {
            _explosionAudioSource.PlayOneShot(_explosionAudioSource.clip);
            car.SlowDown();
            OnExplosion?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
