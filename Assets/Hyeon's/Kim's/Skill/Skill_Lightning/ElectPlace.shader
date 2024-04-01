Shader"Custom/ElectShader"
{
    Properties
    {
        _R ("R", Range(0,1)) = 0.5
        _G ("G", Range(0,1)) = 0.5
        _B ("B", Range(0,1)) = 0.5
        _Bright("bright", Range(0,1)) =0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        float _R;
        float _G;
        float _B;
        float _Bright;

        sampler2D _MainTex;
        
        struct Input
        {
            float2 uv_MainTex;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

    void surf(Input IN, inout SurfaceOutputStandard o)
    {
        o.Emission = float(_R,_G,_B) + _Bright;
    }
        ENDCG
    }
FallBack"Diffuse"
}
