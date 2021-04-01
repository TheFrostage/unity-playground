using System.Collections.Generic;
using UnityEngine;

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
    private Noise _noise;

    private void Start()
    {
        _noise = new Noise((int) Time.time);
        
        _texture2D = new Texture2D(textureSize, textureSize, TextureFormat.R8, false);
        _texture2D.filterMode = FilterMode.Point;
        _texture2D.wrapMode = TextureWrapMode.Clamp;
        for (int i = 0; i < textureSize; i++)
        {
            for (int j = 0; j < textureSize; j++)
            {
                var noiseValue = _noise.Evaluate(new Vector3(i, j, 0) * frequency)* power;
                _texture2D.SetPixel(i, j, Color.white * noiseValue);
            }
        }

        _texture2D.Apply();

        _material.SetTexture("_Noise", _texture2D);
        _material.SetFloat("Speed", speed);

        MaterialPropertyBlock props = new MaterialPropertyBlock();
        foreach (var meshRenderer in MeshRenderers)
        {
            var color = Random.ColorHSV(0, 1, 1,1,1,1,1,1);
            props.SetColor("_Color",color);
            meshRenderer.SetPropertyBlock(props);
        }
        
    }
}