using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.maxValue = maxHealth;
        slider.value = health;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
        Debug.Log(health + "Current");
        Debug.Log(maxHealth + "Max");
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
