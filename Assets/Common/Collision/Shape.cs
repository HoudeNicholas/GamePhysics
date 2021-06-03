using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Shape : MonoBehaviour
{
    public enum eType
    {
        Circle, 
        Box
    }

    public abstract eType type { get; }
    public abstract float mass { get; }
    public abstract float size { get; set; }

    public float density { get; set; } = 1;

    public Color color { set => render.material.color = value; }

    SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
}