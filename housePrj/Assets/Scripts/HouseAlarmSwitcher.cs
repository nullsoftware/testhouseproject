using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAlarmSwitcher : MonoBehaviour
{
    private const string IsThiefInsideHouseProperty = "IsThiefInsideHouse";

    [SerializeField] protected Collider ThiefCollider;
    [SerializeField] protected Animator AlarmLampAnimator;
    [SerializeField] protected AudioSource AlarmSound;
    [SerializeField] protected float AlarmSoundAdjustSpeed = 100f;

    private bool _isThiefInsideHouse;
    private float _currentAlarmVolumePercent = 0;


    private void Update()
    {
        if (_isThiefInsideHouse)
        {
            if (!AlarmSound.isPlaying)
                AlarmSound.Play();

            if (AlarmSound.volume != 1.0f)
            {
                _currentAlarmVolumePercent = Mathf.Min(100f, _currentAlarmVolumePercent + AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolumePercent / 100;
            }
        }
        else
        {
            if (AlarmSound.volume != 0.0f)
            {
                _currentAlarmVolumePercent = Mathf.Max(0f, _currentAlarmVolumePercent - AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolumePercent / 100;
            }

            if (AlarmSound.isPlaying && _currentAlarmVolumePercent == 0.0f)
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