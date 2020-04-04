using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }
    
    [SerializeField]
    private float scrollSpeed;
    private new Renderer renderer;
    private Vector2 savedOffset;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float y = Mathf.Repeat(Time.time * ScrollSpeed, 1);
        Vector2 offset = new Vector2(0, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
