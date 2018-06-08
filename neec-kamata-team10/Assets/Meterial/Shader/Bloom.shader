Shader "Custom/Bloom"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HighLight4x("HighLight4x", 2D) = "black" {}
		_HighLight2x("HighLight2x", 2D) = "black" {}
		_HighLight("HighLight", 2D) = "black" {}
		_Threshold("Threshold", range(0, 1)) = 0
		_Brightness("Brightness", range(0, 1)) = 1
		_LightStrength("LightStrength", range(1, 10)) = 1
		_Offset("Offset", Vector) = ( 0.0, 0.0, 0.0, 0.0 )
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		//HighLight
		Pass
		{
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
			
			sampler2D _MainTex;
			float _Threshold;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 highLight = saturate((col - _Threshold) / (1 - _Threshold));
				return highLight;
			}
			ENDCG
		}
		
		//Blur
		Pass
		{
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Offset;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 sum = fixed4(0, 0, 0, 0);

				int samp = 5;
				int diff = (samp - 1) / 2;
				int gaussian[25] =
					{ 1, 4, 7, 4, 1,

					4, 16, 26, 16, 4,

					7, 26, 41, 26, 7,

					4, 16, 26, 16, 4,

					1, 4, 7, 4, 1 };

				for (int x = -diff; x <= diff; x++)
				{
					for (int y = -diff; y <= diff; y++)
					{
						fixed2 offset = fixed2(x * _Offset.x, y * _Offset.y);
						int rate = gaussian[(x + diff)+(y + diff) * 5];
						sum += tex2D(_MainTex, (i.uv + offset)) * rate;
					}
				}
				fixed4 final = sum / 273.0f;
				return final;
			}
			ENDCG
		}

		//最後の合成
		Pass
		{
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _HighLight;
			sampler2D _HighLight2x;
			sampler2D _HighLight4x;
			float _Brightness;
			float _LightStrength;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 sum = fixed4(0, 0, 0, 0);
				fixed4 final = tex2D(_HighLight4x, i.uv) * 4
					+ tex2D(_HighLight2x, i.uv) * 3 + tex2D(_HighLight, i.uv) * 2;
				final /= 9.0f;

				final = tex2D(_MainTex, i.uv) * _Brightness + final * _LightStrength;

				return final;
			}
			ENDCG
		}
	}
}
