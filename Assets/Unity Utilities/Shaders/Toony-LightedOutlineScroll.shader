Shader "Toon/Lit Outline Scroll" {
	Properties {
	    _MainTex ("Rays texture", 2D) = "white" {}
	    _speed ("Speed", Float) = 0.2
	}

	Category {
	    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }
//	    Blend SrcAlpha One

	    Cull Off Lighting Off ZWrite On Fog { Color (0,0,0,0) }

	    BindChannels {
	        Bind "Color", color
	        Bind "Vertex", vertex
	        Bind "TexCoord", texcoord
	    }

	    SubShader {
	        Pass {
	            CGPROGRAM
	            #pragma vertex vert_img
	            #pragma fragment frag

	            #include "UnityCG.cginc"

	            uniform sampler2D _MainTex;
	            float _speed;

	            float4 frag(v2f_img i) : COLOR {

	                i.uv.y += _Time*_speed;


	                return tex2D(_MainTex, i.uv);
	            }
	            ENDCG
	        }
//
//	        Pass {
//	            CGPROGRAM
//	            #pragma vertex vert_img
//	            #pragma fragment frag
//
//	            #include "UnityCG.cginc"
//
//	            uniform sampler2D _MainTex;
//	            float _speed;
//
//	            float4 frag(v2f_img i) : COLOR {
//
//	                i.uv.y -= sin(_Time*_speed);
//
//	                return tex2D(_MainTex, i.uv);
//	            }
//	            ENDCG
//	        }
	    }
	}
}