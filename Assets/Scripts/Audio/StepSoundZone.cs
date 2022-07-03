using UnityEngine;

public class StepSoundZone : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerSound playerSound))
        {
            playerSound.SwitchStepSound(_stepSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSound playerSound))
        {
            playerSound.SetDefaultStepSound();
        }
    }
}
