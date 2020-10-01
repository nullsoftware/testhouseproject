using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAlarmSwitcher : MonoBehaviour
{
    // flash light animator boolean property name
    private const string IsThiefInsideHouseProperty = "IsThiefInsideHouse";

    [SerializeField] protected Collider ThiefCollider;
    [SerializeField] protected Animator AlarmLampAnimator;
    [SerializeField] protected AudioSource AlarmSound;
    [SerializeField] protected float AlarmSoundAdjustSpeed = 100f;

    private bool _isThiefInsideHouse;
    private float _currentAlarmVolume = 0; // current alarm volume in percent (0% - 100%)


    private void Update()
    {
        if (_isThiefInsideHouse)
        {
            if (!AlarmSound.isPlaying)
                AlarmSound.Play();

            if (AlarmSound.volume != 1.0f)
            {
                _currentAlarmVolume = Mathf.Min(100f, _currentAlarmVolume + AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolume / 100;
            }
        }
        else
        {
            if (AlarmSound.volume != 0.0f)
            {
                _currentAlarmVolume = Mathf.Max(0f, _currentAlarmVolume - AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolume / 100;
            }

            if (AlarmSound.isPlaying && _currentAlarmVolume == 0.0f)
                AlarmSound.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == ThiefCollider)
            AlarmLampAnimator.SetBool(
                IsThiefInsideHouseProperty, _isThiefInsideHouse = true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == ThiefCollider)
            AlarmLampAnimator.SetBool(
                IsThiefInsideHouseProperty, _isThiefInsideHouse = false);
    }
}