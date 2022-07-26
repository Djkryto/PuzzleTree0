using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMoment : MonoBehaviour
{
    public GameObject animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player"){
            animator.SetActive(true);
        }
    }
}
