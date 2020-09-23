using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAlarmController : MonoBehaviour
{
    // flash light animator boolean property name
    private const string IsThiefInsideHouseProperty = "IsThiefInsideHouse";

    [SerializeField] private Collider ThiefCollider;
    [SerializeField] private Animator AlarmLampAnimator;
    [SerializeField] private AudioSource AlarmSound;
    [SerializeField] private float AlarmSoundAdjustSpeed = 100f;

    private bool _isThiefInsideHouse;
    private float _currentAlarmVolume = 0; // current alarm volume in percent (0% - 100%)


    private void Update()
    {
        if (_isThiefInsideHouse)
        {
            // start alaram sound
            if (!AlarmSound.isPlaying)
                AlarmSound.Play();

            // adjusting alarm sound volume to max
            if (AlarmSound.volume != 1.0f)
            {
                _currentAlarmVolume = Mathf.Min(100f, _currentAlarmVolume + AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolume / 100;
            }
        }
        else
        {
            // adjusting alarm sound volume to min
            if (AlarmSound.volume != 0.0f)
            {
                _currentAlarmVolume = Mathf.Max(0f, _currentAlarmVolume - AlarmSoundAdjustSpeed * Time.deltaTime);
                AlarmSound.volume = _currentAlarmVolume / 100;
            }

            // stop alaram sound
            if (AlarmSound.isPlaying && _currentAlarmVolume == 0.0f)
                AlarmSound.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // start flash light animation if thief enter house
        if(other == ThiefCollider)
            AlarmLampAnimator.SetBool(
                IsThiefInsideHouseProperty, _isThiefInsideHouse = true);
    }

    private void OnTriggerExit(Collider other)
    {
        // stop flash light animation if thief exit house
        if (other == ThiefCollider)
            AlarmLampAnimator.SetBool(
                IsThiefInsideHouseProperty, _isThiefInsideHouse = false);
    }
}
