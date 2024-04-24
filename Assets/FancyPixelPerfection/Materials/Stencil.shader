Shader "Custom/Stencil Mask" {
    Properties{
        _SRef("Stencil Ref", Float) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)] _SComp("Stencil Comp", Float) = 8
        [Enum(UnityEngine.Rendering.StencilOp)] _SOp("Stencil Op", Float) = 2
    }
    SubShader{
        Tags { "Queue" = "Geometry-1" "IgnoreProjectors"="True" "RenderType"="Transparent" }
 
        Pass {
            ColorMask 0
            ZWrite off
 
            Stencil
            {
                Ref[_SRef]
                Comp[_SComp]
                Pass[_SOp]
            }
        }
 
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            float4 frag (v2f_img i) : SV_Target
            {
                float2 uv = abs(i.uv * 2.0 - 1.0);
                float mask = max(uv.x, uv.y);
                
                clip(mask);
                return 1;
            }
            ENDCG
        }
    }
}