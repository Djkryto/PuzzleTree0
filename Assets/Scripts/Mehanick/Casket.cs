using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casket : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;

    
    [SerializeField] private List<int> _targetCombination;
    [SerializeField] private List<int> _currentCombination;
    [SerializeField] private List<ButtonCasket> _buttons;
    [SerializeField] private List<MeshRenderer> _indicators;

    [SerializeField] private Material _materialRed;
    [SerializeField] private Material _materialWhite;

    [SerializeField] private Text _textCodeNotepad;

    [SerializeField] private Notepad _notepad;

    [SerializeField] private LearnObject _learnObject;

    [SerializeField] private AudioSource _signSound;
    [SerializeField] private AudioSource _buttonSound;

    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask _layer;

    [SerializeField] private bool _isTargetCombination;

    private UserInput _playerInput;

    private void Awake()
    {
        _playerInput = new UserInput();
        _currentCombination = new List<int>();
        _notepad.enabled = false;
        SetRandomTargetCombination();

        for (int i = 0; i < _targetCombination.Count; i++)
            _textCodeNotepad.text += _targetCombination[i] + "\n";

        foreach (var button in _buttons)
        {
            button.OnPress += CheckCasket;
        }
    }
    // Update is called once per frame
    public void CheckCasket(int buttonNomber)
    {
        _currentCombination.Add(buttonNomber);
        _buttonSound.Play();
        int j = 3 - _currentCombination.Count;
        _indicators[j].material = _materialRed;

        if (_currentCombination.Count == _targetCombination.Count)
        {
            for (int i = 0; i < _currentCombination.Count; i++)
            {
                if (_targetCombination[i] != _currentCombination[i])
                {
                    _isTargetCombination = false;
                }
            }

            if (_isTargetCombination)
            {
                _signSound.enabled = true;
                animator.enabled = true;
                Destroy(GetComponent<Casket>());
                _notepad.enabled = true;
                Destroy(_learnObject);
                gameObject.AddComponent<ReadingObject>();
                Destroy(GetComponent<SphereCollider>());
                _playerInput.Disable();
            }
            else
            {
                _currentCombination.Clear();
                _indicators.ForEach(i => i.material = _materialWhite);
                _isTargetCombination = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _playerInput.Enable();
            _playerInput.Player.Use.performed += context => _playerControl.SetCasketControlState(_layer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            print("Exit");
            _playerInput.Player.Use.performed -= context => _playerControl.SetCasketControlState(_layer);
            _playerInput.Disable();
        }
    }

    private void SetRandomTargetCombination()
    {
        for (int i = 0; i < _targetCombination.Count; i++)
        {
            if(_targetCombination[0] == 1 && _targetCombination[1] == 2)
            {
                i = 0;
            }
            for (int j = 0; j < _targetCombination.Count; j++)
            {
                if (_targetCombination[i] == _targetCombination[j] && i != j)
                {
                    _targetCombination[i] = Random.Range(1, 6);
                }
                else if (i == j)
                {
                    _targetCombination[i] = Random.Range(1, 6);
                    for (int h = 0; h < _targetCombination.Count; h++)
                    {
                        if (_targetCombination[j] == _targetCombination[h] && h != j)
                        {
                            _targetCombination[i] = Random.Range(1, 6);
                            j = 0;
                        }
                    }
                }
            }
        }
    }
}
