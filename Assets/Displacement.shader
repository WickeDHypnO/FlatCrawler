// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders 102/Displacement"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_DisplaceTex("Displacement Texture", 2D) = "white" {}
	_Magnitude("Magnitude", float) = 0.1
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
	sampler2D _DisplaceTex;
	float _Magnitude;

	float dispX;
	float dispY;

	float4 frag(v2f i) : SV_Target
	{
		float2 dispTex = tex2D(_DisplaceTex, i.uv).xy;
		
		
		
		dispX = i.uv.x - 0.5;
		dispY = i.uv.y - 0.5;
		float4 col = tex2D(_MainTex, lerp(i.uv, i.uv + ((abs(dispX) + abs(dispY))*_Magnitude), (i.uv-0.5)*dispTex));
		return col;
	}
		ENDCG
	}
	}
}