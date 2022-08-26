using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioSource _rightLegSource;
    [SerializeField] private AudioSource _leftLegSource;
    [SerializeField] private AudioSource _playerSource;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _takeSound;
    private AudioClip _defaultSound;

    public void Awake()
    {
        _defaultSound = _stepSound;
        SwitchStepSound(_stepSound);
    }

    public void SetDefaultStepSound()
    {
        SwitchStepSound(_defaultSound);
    }

    public void SwitchStepSound(AudioClip clip)
    {
        if(_stepSound != clip)
        {
            _stepSound = clip;
            _leftLegSource.clip = clip;
            _rightLegSource.clip = clip;
        }
    }

    public void PlayRightStepSound()
    {
        if(!_rightLegSource.isPlaying)
            _rightLegSource.PlayOneShot(_stepSound);
    }

    public void PlayLeftStepSound()
    {
        if (!_leftLegSource.isPlaying)
            _leftLegSource.PlayOneShot(_stepSound);
    }

    public void PlayTakeSound()
    {
        _playerSource.PlayOneShot(_takeSound);
    }
}
