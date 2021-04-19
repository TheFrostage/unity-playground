Shader "Kernelics/TrackImage"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Zwrite On
        Tags
        {
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                const float2 center = float2(0.5, 0.5);
                float dist = length(i.uv - center);
                clip(0.5 - dist);
                fixed4 col = tex2D(_MainTex, i.uv);
                float startAlphaDistance = 0.35;
                float endAlphaDistance = 0.5;
                float t = 1 - (dist - startAlphaDistance) / (endAlphaDistance - startAlphaDistance);
                col.a = t;
                return col;
            }
            ENDCG
        }
    }
}