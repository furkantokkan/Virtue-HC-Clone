using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationsManager : MonoBehaviour
{
    public void HapticVibration(HapticTypes type)
    {
        MMVibrationManager.Haptic(type, false, true, this);
    }
}
