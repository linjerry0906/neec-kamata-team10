Shader "Custom/mirror_obj" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

		[Space(50)]
		[IntRange] _StencilRef ("Stencil Reference", Range(0, 255)) = 1
		[Enum(CompareFunction)] _StencilComp ("Stencil Compare Function", Float) = 0

		[Space(50)]
		[Toggle] _Emissive("Emmissive", Float) = 0
		_EmissionColor("EmissionColor", Color) = (0, 0, 0, 0)
		_EmissionMap("Emission", 2D) = "white" {}
		
		[Space(50)]
		[Toggle] _Disappear("Disappear", Float) = 0
		_DesolveTex("DesolveTex", 2D) = "white" {}
		_DesolveTile("DesolveTex", Vector) = (0, 0, 0, 0)
		_ApearColor("ApearColor", Color) = (1,1,1,1)
		_ApearColor2("ApearColor2", Color) = (1,1,1,1)
		_ApearSize("ApearSize", Range(0.0, 1.0)) = 0.3

		[Toggle] _Transparent("Transparent", Float) = 0
	}
	SubShader{
		Tags
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Fade"
		}
		Stencil
		{
			Ref [_StencilRef]
            Comp [_StencilComp]
		}
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma shader_feature _EMISSIVE_ON
#pragma shader_feature _DISAPPEAR_ON
#pragma shader_feature _TRANSPARENT_ON
#pragma surface surf Standard fullforwardshadows // alpha:fade 

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DesolveTex;
		sampler2D _EmissionMap;

		struct Input
		{
			float2 uv_MainTex;
		};

		fixed4 _Color;
		fixed4 _EmissionColor;
		fixed4 _ApearColor;
		fixed4 _ApearColor2;
		float4 _DesolveTile;
		half _ApearSize;
		half _Disappear;
		half _Glossiness;
		half _Metallic;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			#if _TRANSPARENT_ON 
			if(c.a < 0.001)
				discard;
			#endif

			#if _DISAPPEAR_ON
				fixed4 mask = tex2D(_DesolveTex, IN.uv_MainTex * _DesolveTile.xy);
				
				half dBase = -2.0f * (1 - c.a) + 1.0f;			//alpha 0 => -1 , alpha 1 => 1
				half dTexRead = mask.r + dBase;					//c.aで制御
				half alpha = clamp(dTexRead, 0.0f, 1.0f);
				if(alpha < 0.001)
					discard;
				o.Albedo = c.rgb;
				o.Alpha = alpha;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;

				if (alpha < _ApearSize)
				{
					fixed4 light = lerp(_ApearColor, _ApearColor2, abs(alpha - mask.r) / _ApearSize);
					o.Albedo = light;
					o.Emission = light;
					return;
				}

				fixed4 emiss = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
				o.Emission = emiss;
				return;
			#endif

			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			
			#if _EMISSIVE_ON
			fixed4 e = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
			o.Emission = e;
			#endif
		}
		ENDCG
	}
	FallBack "Diffuse"
}
