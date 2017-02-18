using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempGauge : MonoBehaviour {
    private Slider slider;

    public Image Fill;
    public Color Hot = Color.yellow;
    public Color Cold = Color.red;
	// Use this for initialization
	void Start () {
        slider = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        Fill.color = Color.Lerp(Cold, Hot, (float)slider.value / slider.maxValue);
    }
}
