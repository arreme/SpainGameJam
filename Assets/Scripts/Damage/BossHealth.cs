﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Damageable
{
    public Slider slider;
    public float sliderSmooth = 0.15f;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    new private void Awake()
    {
        base.Awake();
        slider.enabled = true;
        slider.maxValue = maxHealth;
        slider.value = slider.maxValue;
    }

    private void FixedUpdate()
    {
        SliderUpdate();
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
    }

    private void SliderUpdate()
    {
        slider.value = Mathf.Lerp(slider.value, currentHealth, sliderSmooth);
    }

    public override void Kill()
    {
        base.Kill();
    }
}
