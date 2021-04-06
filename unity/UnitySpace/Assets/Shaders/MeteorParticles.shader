Shader "Kernelics/MeteorParticles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Power ("Power",float) = 1
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 N : TEXCOORD1;
                float4 color : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.N = UnityObjectToWorldNormal(v.normal);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                float3 L = _WorldSpaceLightPos0;
                float lamberColor = dot(L, i.N);
                color *= lamberColor;
                color *= i.color;

                return color;
            }
            ENDCG
        }
    }
}