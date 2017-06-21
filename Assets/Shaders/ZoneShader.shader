Shader "Custom/ZoneShader" {
	Properties {
		_BorderColor ("Border color", Color) = (1,1,1,1)
		_ZoneColor ("Zone color", Color) = (0.5,0.5,0.5,1)
		_Depth ("Border size", Range(0,1)) = 0.5
		_Size ("Size", Float) = 0.5
		_MyTexture ("Texture", 2D) = "white" {} 
		
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
            
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			fixed4 _BorderColor;
			fixed4 _ZoneColor;
			float _Depth;
			float _Size;
			sampler2D _MyTexture;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR0;
				float2 extra : TEXCOORD0;
			};

			v2f vert (appdata v)
			{	
				v2f o;

				
				float4 foo = {0, 0, 0, 0};
				float curDist = distance(v.vertex, foo);
				float gradientK = curDist/_Size;

				if(gradientK > 1) {
					gradientK = 1;
				}

				float dR = _BorderColor[0] - _ZoneColor[0];
				float dG = _BorderColor[1] - _ZoneColor[1];
				float dB = _BorderColor[2] - _ZoneColor[2];
				float dA = _BorderColor[3] - _ZoneColor[3];

				float4 color = { _ZoneColor[0] + gradientK*dR, _ZoneColor[1] + gradientK*dG, _ZoneColor[2] + gradientK*dB, _ZoneColor[3] + gradientK*dA };
				o.color = color;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{	
				float dR = _ZoneColor[0] - _BorderColor[0];
				float dG = _ZoneColor[1] - _BorderColor[1];
				float dB = _ZoneColor[2] - _BorderColor[2];
				float dA = _ZoneColor[3] - _BorderColor[3];

				float intensity = abs(_BorderColor[0] - i.color[0])/abs(dR);

				if (intensity > _Depth) {
					return _ZoneColor;
				}

				return _BorderColor;
				//float4 color = { _BorderColor[0] + intensity*dR, _BorderColor[1] + intensity*dG, _BorderColor[2] + intensity*dB, _BorderColor[3] + intensity*dA };
				//return color;
			}
		
			ENDCG
		}
	}
}
