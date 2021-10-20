  Shader "Edge Length Tessellation" {
    Properties {
        _EdgeLength ("Edge length", Range(2,50)) = 15
        _TextureScale ("Texture Scale", Range(0.125, 8.0)) = 1
        _TextureOffset ("Texture Offset", Range(-1.0, 1.0)) = 0
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _DispTex ("Disp Texture", 2D) = "gray" {}
        _NormalMap ("Normalmap", 2D) = "bump" {}
        _DisplacementScale ("Displacement Scale", Range(0, 1.0)) = 0.5
        _DisplacementOffset ("Displacement Offset", Range(-10.0, 10.0)) = 0.0
        _Color ("Color", color) = (1,1,1,0)
        _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 300
        
        CGPROGRAM
        #pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessEdge nolightmap
        #pragma target 4.6
        #include "Tessellation.cginc"

        struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };

        float _EdgeLength;

        float4 tessEdge (appdata v0, appdata v1, appdata v2)
        {
            return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
        }

        float _TextureScale;
        float _TextureOffset;

        sampler2D _DispTex;
        float _DisplacementScale;
        float _DisplacementOffset;

        void disp (inout appdata v)
        {
            float d = tex2Dlod(_DispTex, float4(v.texcoord.xy / _TextureScale + _TextureOffset,0,0)).r * _DisplacementScale + _DisplacementOffset;
            d *= _TextureScale;
            v.vertex.xyz += v.normal * d;
        }

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        sampler2D _NormalMap;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex.xy / _TextureScale + _TextureOffset) * _Color;
            o.Albedo = c.rgb;
            o.Specular = 0.2;
            o.Gloss = 1.0;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex / _TextureScale + _TextureOffset)).xyz;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
