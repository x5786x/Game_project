using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float time, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0;
        while(timer <= time)
        {
            float x = Random.Range(-2f, 2f) * magnitude;

            transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
