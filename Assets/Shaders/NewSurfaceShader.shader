Shader "Custom/NewSurfaceShader" {
	Properties {
        _AmbientLightColor ("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity ("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0
		_AMOUNT_OF_COLORS ("Amount of colors", Int) = 5
    }
	SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

		Pass {
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members distance)
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
            
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			fixed4 _AmbientLightColor;
            float _AmbientLighIntensity;

			

			int _AMOUNT_OF_COLORS;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 _distance : TEXCOORD0;
			};
            
			v2f vert (appdata v)
			{	
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float _distance = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex));


				half3 worldNormal = UnityObjectToWorldNormal(v.normal);

				float nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

				float2 temp = { _distance, nl };

				o._distance = temp;

				
                

				return o;
			}
            
			fixed4 frag (v2f i) : SV_Target
			{	
				float _distance = i._distance[0];

				//float tempK = (_distance - _minDistance) / (_maxDistance - _minDistance);

				float _dot = i._distance[1];
				//float intensity = tempK;
				
				_dot = round(_dot*_AMOUNT_OF_COLORS)/_AMOUNT_OF_COLORS;

				

				//if (false && _distance > _BASE_DIST) {
				//	intensity = 0;
				//} else {
				//	if(_distance <= 0) {
				//		intensity = 1;
				//	} else {
				//		intensity = (_BASE_DIST - _distance) / _BASE_DIST;
				//	}	
				//}

				fixed4 bar = _dot * _AmbientLightColor;
				//tempK >= 0 && tempK <= 1
				//if(_maxDistance < _distance) {
				//	bar *= 10000;
				//}

				return bar;
			}
			ENDCG
		}
	}
}
