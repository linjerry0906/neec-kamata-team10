﻿Shader "Custom/mirror_stencil" {
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { "Queue" = "Geometry-1" }
		ColorMask 0
		ZWrite off
		Pass{
		Stencil
			{
				Ref 1
				Comp always
				Pass replace
			}
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		struct appdata
		{
			float4 vertex : POSITION;
		};
		struct v2f
		{
			float4 pos : SV_POSITION;
		};

		v2f vert(appdata v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}

		fixed4 _Color;

		half4 frag(v2f i) : SV_Target
		{
			return _Color;
		}
		ENDCG
		}
	}
	FallBack "Diffuse"
}
