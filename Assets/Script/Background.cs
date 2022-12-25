using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Background : MonoBehaviour
{
    Vector2 resolution;
    public RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        resolution = transform.root.GetComponent<CanvasScaler>().referenceResolution;
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = (Screen.width * resolution.x)/(Screen.height * resolution.y);
        rect.sizeDelta *= ratio;
    }
}
