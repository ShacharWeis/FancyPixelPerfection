Shader "Custom/MaskFieldPos" {
    Properties{
    [HideInInspector] _MainTex("Base (RGB)", 2D) = "white" { }
    }
        SubShader{
        // Render the mask after regular geometry, but before masked geometry and
        // transparent things.
 
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
 
        // Don't draw in the RGBA channels; just the depth buffer
 
        ColorMask 0
        ZWrite On
        Cull Off
        Lighting Off
 
        Blend One One
        // Do nothing specific in the pass:
 
        Pass {}
    }
}