Shader "Unlit/ColorSwap"
{
    Properties
    {
        [NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        //sampler2D _MainTex = sampler2D { Texture = white };
        _ReplacementColor ("Replacement Color", Color) = (1, 0, 0, 1)
        _TargetColor ("Target Color", Color) = (0, 0, 1, 1)
    }

    SubShader
    {
        // configuration parameters
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        // specify Blend computations for source and target fragments
        Blend SrcAlpha OneMinusSrcAlpha

        // disable 3D-specific processes
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma fragment frag
            #pragma vertex vert
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _ReplacementColor;
            fixed4 _TargetColor;

            // vertex shader
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
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            // main fragment shader
            fixed4 frag (float2 uv : TEXCOORD0) : SV_TARGET
            {
                fixed4 color = tex2D (_MainTex, uv);

                // check if the pixel color matches the target color
                if (all(color.rgb == _TargetColor.rgb))
                {
                    // replaces current pixel color with replacement color
                    color.rgba = _ReplacementColor.rgba;
                }

                return color;
            }

            ENDCG
        }
    }
}
