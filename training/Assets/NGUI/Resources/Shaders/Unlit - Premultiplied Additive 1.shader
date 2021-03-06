Shader "Hidden/Unlit/Premultiplied Additive 1"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			AlphaTest Off
			Fog { Mode Off }
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY ALPHA_SPLIT_ENABLED
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
			float4 _ClipArgs0 = float4(1000.0, 1000.0, 0.0, 1.0);

			#ifdef ALPHA_SPLIT_ENABLED
			sampler2D _AlphaTex;
			#endif

			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 worldPos : TEXCOORD1;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				o.worldPos = v.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0.xy;
			
				// Sample the texture
				half4 col = tex2D(_MainTex, IN.texcoord) * IN.color;
				
				#ifdef ALPHA_SPLIT_ENABLED
				col.a = tex2D( _AlphaTex, IN.texcoord ).r * IN.color.a;
				#endif

				col.rgb *= col.a;

				float fade = clamp( min(factor.x, factor.y), 0.0, 1.0);
				col.a *= fade;
				col.rgb = lerp(half3(0.0, 0.0, 0.0), col.rgb, fade);
				return col;
			}
			ENDCG
		}
	}
	Fallback "Unlit/Premultiplied Colored"
}
