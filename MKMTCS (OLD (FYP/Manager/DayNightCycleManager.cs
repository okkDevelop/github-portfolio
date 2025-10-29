using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    [Header("Sky Setting")]
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Light sunLight;
    [SerializeField] private RectTransform arrowPivot;
    [SerializeField] private float duration = 300f;
    [SerializeField] private float transitionSpeed = 0.5f;

    // Update is called once per frame
    private void Update()
    {
        ArrowRotating();
        DayNightTriggerEvent(ArrowRotating());
    }

    private float ArrowRotating()
    {
        float _time = 360f / duration * Time.deltaTime;
        arrowPivot.Rotate(0f, 0f, _time);
        return _time;
    }

    private void DayNightTriggerEvent(float _dayNightAdjusting) 
    {
        float _rotation = arrowPivot.localEulerAngles.z;
        float _originalHungerRate = PlayerStatus.Instance.HungerRate;

        if (_rotation >= 0 && _rotation <= 180)
        {
            PlayerStatus.Instance.HungerRate = _originalHungerRate;
        }
        else if (_rotation > 180 && _rotation <= 360)
        {
            PlayerStatus.Instance.HungerRate = 0.7f;

            if (_rotation == 360) 
            {
                _rotation = 0f;
            }
        }

        float dayNightFactor = 0f;

        if (_rotation >= 0 && _rotation <= 180)
        {
            dayNightFactor = Mathf.InverseLerp(0, 180, _rotation);
        }
        else if (_rotation > 180 && _rotation <= 360)
        {
            dayNightFactor = Mathf.InverseLerp(360, 180, _rotation);
        }

        Color targetColor = Color.Lerp(Color.black, Color.white, dayNightFactor);
        sunLight.color = targetColor;
        skyboxMaterial.SetColor("_Tint", targetColor);
    }
}
