Shader "Kernelics/MeteorAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Texture", 2D) = "white"{}
        _powerOfWave("powerOfWave", float) = 1
        _speed("speed",float) = 1
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
            #include "Lighting.cginc"
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
            };

            sampler2D _MainTex;
            sampler2D _Noise;
            float4 _MainTex_ST;
            float _powerOfWave;
            float _speed;

            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }

            v2f vert(appdata v)
            {
                v2f o;

                float4 power = tex2Dlod(_Noise, float4(frac(v.uv + _Time.yy * _speed), 0, 0));
                v.vertex += v.vertex * power * _powerOfWave;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.N = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color = 1;
                float3 L = _WorldSpaceLightPos0;
                float lamberColor = dot(L, i.N);
                float height = tex2Dlod(_Noise, float4(frac(i.uv + _Time.yy * _speed), 0, 0)) / 4 + 0.75;
                color *= lamberColor * height;

                //return float4(i.uv.xy,0,1);
                return float4(color.xyz, 1);
            }
            ENDCG
        }
    }
}