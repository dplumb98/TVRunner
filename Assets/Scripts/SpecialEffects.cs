using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpecialEffects : MonoBehaviour
{
    public static bool playerEffected = false;
    public Volume normalVisionVolume;
    public Volume distortedVisionVolume;
    public AudioReverbZone audioReverb;
    public float specialEffectDuration = 10;
    private float playerEffectedCounter = 0;

    ColorAdjustments colorAdj;

    private void Start()
    {
        distortedVisionVolume.profile.TryGet<ColorAdjustments>(out colorAdj); // Grab our Color Adjustment override
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEffected == true)
        {
            normalVisionVolume.enabled = false;
            distortedVisionVolume.enabled = true;
            colorAdj.hueShift.value = Mathf.PingPong(Time.time * 6, 180); // Shift our hue to create crazy colors
            audioReverb.reverbPreset = AudioReverbPreset.Psychotic;
            if (playerEffectedCounter == 0)
            {
                StartCoroutine(EffectDuration());
            }
            playerEffectedCounter++;
        }
        else
        {
            normalVisionVolume.enabled = true;
            distortedVisionVolume.enabled = false;
            audioReverb.reverbPreset = AudioReverbPreset.Generic;
            playerEffectedCounter = 0;
        }
    }

    private IEnumerator EffectDuration()
    {
        yield return new WaitForSeconds(specialEffectDuration);
        playerEffected = false;
    }
}
