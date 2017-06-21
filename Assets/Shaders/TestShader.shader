// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader "Custom/TestShader" {
	Properties {
        _AmbientLightColor ("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity ("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0
		_BASE_DIST ("Base distance", Range(0.0, 15.0)) = 10.0
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

			float _BASE_DIST;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 _distance : TEXCOORD0;
				float4 diff : COLOR0;
			};
            
			v2f vert (appdata v)
			{	
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float _distance = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex));
				float2 temp = { _distance, 0 };
				o._distance = temp;

				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                
                o.diff = nl * _LightColor0;

				return o;
			}
            
			fixed4 frag (v2f i) : SV_Target
			{	
				float foo = i._distance[0];
				

				float intensity;

				if (foo > _BASE_DIST) {
					intensity = 0;
				} else {
					if(foo <= 0) {
						intensity = 1;
					} else {
						intensity = (_BASE_DIST - foo) / _BASE_DIST;
					}	
				}

				fixed4 bar =  i.diff * _AmbientLightColor * round(5 * _SinTime);

				return bar;
			}
			ENDCG
		}
	}
}
