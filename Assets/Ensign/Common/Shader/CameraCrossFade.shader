Shader "Ensign/Camera/Cross Fade"
{
    Properties
    {
        _MainTex("Default Camera", 2D) = "white" {}
        _TransitionTex("Transtition Camera", 2D) = "white" {}
        _BlendAmount("Blend Amount", Range(0.0, 1.0)) = 1.0
    }
    
    CGINCLUDE
    #include "UnityCG.cginc"
    
    sampler2D _MainTex;
    sampler2D _TransitionTex;
    
    fixed _BlendAmount;
    
    float4 frag_blend(v2f_img i) : COLOR
    {
        return float4(lerp(tex2D(_MainTex, i.uv).rgb, tex2D(_TransitionTex, i.uv).rgb, _BlendAmount), 1.0);
    }
    ENDCG
    
    SubShader
    {
        Tags
        {
            "PreviewType"="Plane"
        }
        
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode off }
        
        Pass
        {
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma target 2.0
            #pragma vertex vert_img
            #pragma fragment frag_blend
            ENDCG
        }
    }
    
    FallBack off
}