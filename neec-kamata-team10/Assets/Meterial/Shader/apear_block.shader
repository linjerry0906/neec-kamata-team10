﻿Shader "Custom/apear_block" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DesolveTex("DesolveTex", 2D) = "white" {}

		_EmissionColor("EmissionColor", Color) = (0, 0, 0, 0)
		_EmissionMap("Emission", 2D) = "white" {}

		_ApearColor ("ApearColor", Color) = (1,1,1,1)
		_ApearColor2 ("ApearColor2", Color) = (1,1,1,1)
		_ApearSize ("ApearSize", Range(0.0, 1.0)) = 0.3
	}
	SubShader {
		Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Fade"
		}
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DesolveTex;
		sampler2D _EmissionMap;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		fixed4 _EmissionColor;
		fixed4 _ApearColor;
		fixed4 _ApearColor2;
		half _ApearSize;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 mask = tex2D(_DesolveTex, IN.uv_MainTex);

			half dBase = -2.0f * (1 - c.a) + 1.0f;			//alpha 0 => -1 , alpha 1 => 1
			half dTexRead = mask.r + dBase;					//c.aで制御
			half alpha = clamp(dTexRead, 0.0f, 1.0f);
			o.Albedo = c.rgb;
			o.Alpha = alpha;


			if (alpha < _ApearSize)
			{
				fixed4 light = lerp(_ApearColor, _ApearColor2, abs(alpha - mask.r) / _ApearSize);
				o.Albedo = light;
				o.Emission = light;
				return;
			}

			fixed4 e = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
			o.Emission = e;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
