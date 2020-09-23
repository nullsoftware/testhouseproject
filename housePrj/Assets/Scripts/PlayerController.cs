using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<KeyCode, Action> Actions { get; } // keyboard keys to actions bindings
    private Rigidbody _rigidbody; // thief rigitbody

    private KeyCode? _currentAction = null; // current action key

    [SerializeField] private float Speed = 5f; // thief speed

    public PlayerController()
    {
        // actions initialization
        Actions = new Dictionary<KeyCode, Action>()
        {
            [KeyCode.A] = () => _rigidbody.velocity = Vector3.left * Speed,
            [KeyCode.D] = () => _rigidbody.velocity = Vector3.right * Speed,
        };
    }

    private void Awake()
    {
        // fields initialization
        _rigidbody = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool found = false; // is key found during checking

        // checking every key stored in Actions dictionary for pressing
        foreach (KeyCode key in Actions.Keys)
        {
            if(Input.GetKey(key))
            {
                found = true;

                // if current action the same as found action - exit checking
                if (_currentAction == key)
                    break;

                Actions[key](); // execute key action
                _currentAction = key; // set current key as current action
                break;
            }
        }

        // if user press no key from Actions dictionary - current action should be empty
        if (!found)
            _currentAction = null;

        // if no current action - stop player
        if (!_currentAction.HasValue)
            _rigidbody.velocity = Vector3.zero;
    }

    private void OnGUI()
    {
        //
        // drawing "Thief" text above player
        //

        Vector3 screenPos = Camera.main.WorldToScreenPoint(_rigidbody.transform.position);

        GUIStyle textStyle = new GUIStyle()
        {
            fontSize = 16,
            active = new GUIStyleState() { textColor = Color.red },
            normal = new GUIStyleState() { textColor = Color.red },
        };

        GUI.Label(new Rect(screenPos.x - 20, Screen.height - (screenPos.y + 60), 100, 50), "Thief", textStyle);
    }


}
