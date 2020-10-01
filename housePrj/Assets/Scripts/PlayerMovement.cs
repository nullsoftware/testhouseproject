using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float Speed = 5f;

    private Rigidbody _rigidbody;

    private KeyCode? _currentAction = null;

    private Dictionary<KeyCode, Action> Actions { get; } // keyboard keys to actions bindings
      

    public PlayerMovement()
    {
        Actions = new Dictionary<KeyCode, Action>()
        {
            [KeyCode.A] = () => _rigidbody.velocity = Vector3.left * Speed,
            [KeyCode.D] = () => _rigidbody.velocity = Vector3.right * Speed,
        };
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool actionFound = false;

        foreach (KeyCode key in Actions.Keys)
        {
            if(Input.GetKey(key))
            {
                actionFound = true;

                if (_currentAction == key)
                    break;

                Actions[key]();
                _currentAction = key;
                break;
            }
        }

        if (!actionFound)
            _currentAction = null;

        if (!_currentAction.HasValue)
            _rigidbody.velocity = Vector3.zero;
    }

}