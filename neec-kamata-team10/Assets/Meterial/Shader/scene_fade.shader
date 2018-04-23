Shader "Custom/Custom/SceneFade"
{
	Properties
	{
		_FaderMask("Mask", 2D) = "white" {}
		_MainTex("LastScene", 2D) = "white" {}
		_Factor("Factor", Range(0,1)) = 0.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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
			
			sampler2D _FaderMask;
			sampler2D _MainTex;
			half _Factor;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 mask = tex2D(_FaderMask, i.uv);
				
				if (mask.r <= _Factor)
					discard;

				col.a = 1.0;
				return col;
			}
			ENDCG
		}
	}
}
