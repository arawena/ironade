Shader "Custom/wireFrameShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
         Pass {
             ZWrite On
             ZTest LEqual
             Cull Off
             Fog { Mode Off }
             BindChannels {
                 Bind "vertex", vertex Bind "color", color
             }
         }
     }
}
