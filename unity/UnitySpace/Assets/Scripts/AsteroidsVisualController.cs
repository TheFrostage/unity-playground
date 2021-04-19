using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class AsteroidsVisualController : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    [SerializeField]
    private Texture2D _texture2D;

    [SerializeField, Range(1, 1024)]
    private int textureSize;

    [SerializeField, Range(0.0001f, 10)]
    private float frequency;

    [SerializeField, Range(0.0001f, 10)]
    private float power;

    [SerializeField, Range(0.0001f, 10)]
    private float speed;

    [SerializeField]
    private List<MeshRenderer> MeshRenderers;

    /* [SerializeField]
     private SpriteRenderer _sprite;*/

    private void Start()
    {

        
    }
}