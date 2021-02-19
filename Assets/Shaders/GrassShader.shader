Shader "Unlit/GrassShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="Universal"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
              struct Attributes
            {
                float4 positionOS   : POSITION;                 
            };

             struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

             Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag() : SV_Target
            {
                Light light = GetMainLight();
                // Returning the _BaseColor value.                
                return 1;
            }
            ENDCG
        }
    }
}
