using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLightStrength : MonoBehaviour
{
    public Light lightObj;
    public float intensity = 6f;
    public float intensityMultiplier = 5f;

    private void Start()
    {
        lightObj = GetComponent<Light>();
        lightObj.intensity -= Time.deltaTime;
    }

    // Update is called once per frame
    public void DecreaseLightIntensity()
    {
        StartCoroutine(DecreaseLight());
    }

    public void SetLightIntensity()
    {
        lightObj.intensity = intensity;
    }

    public void IncreaseLightIntensity()
    {
        StartCoroutine(DecreaseLight());
    }

    private IEnumerator DecreaseLight()
    {
        yield return new WaitForFixedUpdate();
        lightObj.intensity -= Time.deltaTime* intensityMultiplier;
        if (lightObj.intensity > 0)
        {
            StartCoroutine(DecreaseLight());
        }
        
    }

    private IEnumerator IncreaseLight()
    {
        yield return new WaitForFixedUpdate();
        lightObj.intensity += Time.deltaTime * intensityMultiplier;
        if (lightObj.intensity >= intensity)
        {
            StartCoroutine(IncreaseLight());
        }

    }
}
