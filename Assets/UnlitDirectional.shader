// Shader cieniujący face obiektu na podstawie sprecyzowanego wektora
Shader "Custom/FaceDirectionShadowing" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_BaseColor("Base Color", Color) = (0.0,0.0,0.0,1.0)
		_LightColor("Light Color", Color) = (1.0,1.0,1.0,1.0)
		_LightFactor("Light Amount", Range(0,5)) = 2
		_ShadowDirection("Shadow Direction", Vector) = (1.25,5,0)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf BlinnPhong vertex:myvert
#include "UnityCG.cginc"

	float4 _BaseColor;
	float4 _LightColor;
	float3 _ShadowDirection;
	half _LightFactor;
	sampler2D _MainTex;

	struct Input {
		float3 normalW;
		float2 uv_MainTex;
	};

	void myvert(inout appdata_full v, out Input data) {
		UNITY_INITIALIZE_OUTPUT(Input,data);
		data.normalW = mul((float3x3)unity_ObjectToWorld, v.normal);
	}

	void surf(Input IN, inout SurfaceOutput o) {
		float ShadowValue = (dot(normalize(IN.normalW), _ShadowDirection.xyz)) / 2;
		half4 result = 0;
		result = smoothstep(0,1,ShadowValue);
		o.Albedo = result * _BaseColor * (_LightColor*_LightFactor) * tex2D(_MainTex, IN.uv_MainTex);
		o.Alpha = 0;
	}
	ENDCG
	}
		FallBack "Diffuse"
}