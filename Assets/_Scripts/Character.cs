using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;
    public float flashSpeed = 5f;
    public float invulnerabilityTime;
    public bool isVulnerable;
    public SliderJoint2D healthSlider;
    public Image damageImage;
    public Color flashColour = new Color();
}
