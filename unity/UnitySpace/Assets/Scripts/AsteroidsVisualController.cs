using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsVisualController : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    [SerializeField]
    private Texture2D _texture2D;

    [SerializeField, Range(1,1024)]
    private int textureSize;
    
    [SerializeField, Range(1,1024)]
    private int frequency;
    void Start()
    {
        _texture2D = new Texture2D(textureSize, textureSize, TextureFormat.R8, false);
        _texture2D.filterMode = FilterMode.Point;
        _texture2D.wrapMode = TextureWrapMode.Clamp;

        for (int i = 0; i < textureSize; i++)
        {
            for (int j = 0; j < textureSize; j++)
            {
                float noise = Noise.Perlin2D(new Vector3(i/(float)textureSize, j/(float)textureSize, 0f), frequency);
                _texture2D.SetPixel(i,j,Color.white * noise);
            }
        }
        
        _texture2D.Apply();

        _material.SetTexture("_Noise", _texture2D);
        
    }
}
