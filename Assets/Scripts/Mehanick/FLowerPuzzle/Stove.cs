using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] private List<Stove> _stoves;
    [SerializeField] private AudioSource _audioSource;
    public PuzzleStove puzzleStove;
    public Vector3 TargetPosition;
    private float timerState;//Таймер проверки ухода объекта с блина.
    public bool CloseTimer;
    public bool isStay;
    public bool isActive;
    public bool isLock;

    // Update is called once per frame
    public void Start()
    {
        puzzleStove = transform.parent.GetComponent<PuzzleStove>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isActive)
        {
            timerState = 0.5f;
            CloseTimer = false;
            TargetPosition = new Vector3(transform.position.x, 4.875f, transform.position.z);
            if (TargetPosition.y < transform.position.y)
            {
                transform.position -= transform.up * Time.deltaTime;
            }
        }
        else
        {
            if (!CloseTimer)
            {
                timerState -= Time.deltaTime;
            }

            if (timerState < 0)
            {
                TargetPosition = new Vector3(transform.position.x, 4.98f, transform.position.z);
                if (TargetPosition.y > transform.localPosition.y)
                {
                    transform.position += transform.up * Time.deltaTime;
                    CloseTimer = true;
                }

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "RockBig" || collision.collider.tag == "Player")
        {
            _audioSource.Play();
            isStay = true;
            if (!isActive)
            {
                isActive = true;
                for (int i = 0; i < _stoves.Count; i++)
                {
                    _stoves[i].isActive = true;
                }
            }
            isActive = true;
            puzzleStove.Check(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "RockBig" || collision.collider.tag == "Player")
        {
            isStay = false;
            if (isActive)
            {
                _audioSource.Play();
                for (int i = 0; i < _stoves.Count; i++)
                {
                    if(!_stoves[i].isStay)
                        _stoves[i].isActive = false;
                }
            }
            isActive = false;
        }
    }
}
