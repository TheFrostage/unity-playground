// Upgrade NOTE: upgraded instancing buffer 'InstanceProperties' to new syntax.

Shader "Kernelics/MeteorAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Clip("Clip",float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"


            struct appdata
            {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 N : TEXCOORD1;
                float3 worldVertex : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float Speed;
            float _Clip;
            
            UNITY_INSTANCING_BUFFER_START(Props)
	        UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)


            float _coef1;

            v2f vert(appdata v)
            {               
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                float3 worldVert = mul(unity_ObjectToWorld, v.vertex);
                float3 center = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
                float3 fromCameraToVertex = worldVert - _WorldSpaceCameraPos;
                float3 fromVertexToCenter = center - worldVert;
               
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.N = UnityObjectToWorldNormal(v.normal);
                o.worldVertex = worldVert;
                o.uv = 0;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                float4 color = tex2D(_MainTex, i.uv);
                //clip(noise - _Clip);
                float3 L = _WorldSpaceLightPos0; 
                float lamberColor = dot(L, i.N);
                //clip(height + _clipCoef); 
                color *= lamberColor;
                color *= UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgba;

                return color;
            }
            ENDCG
        }
    }
}