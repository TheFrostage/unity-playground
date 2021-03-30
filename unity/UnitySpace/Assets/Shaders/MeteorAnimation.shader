﻿Shader "Kernelics/MeteorAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color",Color) = (1,1,1,1)
        _Clip("Clip",float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 N : TEXCOORD1;
                float3 worldVertex : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _Noise;
            float4 _MainTex_ST;
            float Speed;
            float _Clip;


            float _coef1;

            v2f vert(appdata v)
            {
                v2f o;
                float3 worldVert = mul(UNITY_MATRIX_M, v.vertex);
                float4 power = tex2Dlod(_Noise, float4(worldVert.xy + _Time.yy * Speed, 0, 0));
                v.vertex += float4(v.normal * power.x, 0);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.N = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color =  tex2D(_MainTex,i.uv);
                float noise = 1 - tex2D(_Noise, frac(i.N + _Time.xx)).x;
                clip(noise - _Clip);
                float3 L = _WorldSpaceLightPos0;
                float lamberColor = dot(L, i.N);
                //clip(height + _clipCoef);
                color*=lamberColor;

                 return color;
                //return fixed4(i.uv.xy, 0, 1);
            }
            ENDCG
        }
    }
}