

float2 FlipbookUV(float2 uv, float2 flipbookSize, float spriteIndex)
{
	// get single sprite size
	float2 size = float2(1.0 / flipbookSize.x, 1.0 / flipbookSize.y);
	uint totalFrames = flipbookSize.x * flipbookSize.y;

	// wrap x and y indexes
	uint indexX = spriteIndex % flipbookSize.x;
	uint indexY = floor((spriteIndex % totalFrames) / flipbookSize.x);

	// get offsets to our sprite index
	float2 offset = float2(size.x * indexX, -size.y * indexY);

	// get single sprite UV
	float2 innerUV = uv * size;// *float2(0.9, 1);

	// Shrink the inner UV i tiny bit
	// This is because the texture sampler will sample a tiny bit from the next flipbook sprite and that leaves visual artifacts most of the time. 
	// Try adjusting 'shrinking' if you get strange "texture leakage"
	float shrinking = 0.05;
	float2 shrinkingXY = float2(shrinking / flipbookSize.x, shrinking / flipbookSize.y);
	innerUV = innerUV * float2(1 - shrinkingXY.x, 1 - shrinkingXY.y);

	// flip Y (to start 0 from top)
	innerUV.y = innerUV.y + size.y * (flipbookSize.y - 1);
	//innerUV = innerUV + offset;
	// Adjust for the shrinking
	innerUV = innerUV + offset + float2((shrinkingXY.x / 2) / flipbookSize.x, (shrinkingXY.y / 2) / flipbookSize.y);

	return innerUV;
}

void ParalaxInterior_CubeTextureSampler_float(float3 direction, const SamplerState samplerTex, Texture2D leftWall, Texture2D rigtWall, Texture2D floor, Texture2D ceiling, Texture2D backWall, out float4 baseColor)
{
	float faceIndex;
	float2 uv;

	float3 vAbs = abs(direction);
	float ma;
	if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y)
	{
		faceIndex = direction.z < 0.0 ? 5.0 : 4.0;
		ma = 0.5 / vAbs.z;
		uv = float2(direction.z < 0.0 ? -direction.x : direction.x, -direction.y);
	}
	else if (vAbs.y >= vAbs.x)
	{
		faceIndex = direction.y < 0.0 ? 3.0 : 2.0;
		ma = 0.5 / vAbs.y;
		uv = float2(direction.x, direction.y < 0.0 ? -direction.z : direction.z);
	}
	else
	{
		faceIndex = direction.x < 0.0 ? 1.0 : 0.0;
		ma = 0.5 / vAbs.x;
		uv = float2(direction.x < 0.0 ? direction.z : -direction.z, -direction.y);
	}
	uv = uv * ma + 0.5;
	uv.x = 1 - uv.x;

	baseColor = float4(0, 0, 0, 0);
	if (faceIndex == 0)
	{
		baseColor = SAMPLE_TEXTURE2D(leftWall, samplerTex, uv);
	}
	else if (faceIndex == 1)
	{
		baseColor = SAMPLE_TEXTURE2D(rigtWall, samplerTex, uv);	
	}
	else if (faceIndex == 2)
	{
		baseColor = SAMPLE_TEXTURE2D(floor, samplerTex, uv);
	}
	else if (faceIndex == 3)
	{
		baseColor = SAMPLE_TEXTURE2D(ceiling, samplerTex, uv);
	}
	else if (faceIndex == 5)
	{
		baseColor = SAMPLE_TEXTURE2D(backWall, samplerTex, uv);
	}
}

void ParalaxInterior_CubeTextureSampler_Flipbook_float(float3 direction, float2 flipbookSize, float4 spriteIndicesLRFC, float spriteIndexB, const UnitySamplerState samplerTex, UnityTexture2D leftWall, UnityTexture2D rightWall, UnityTexture2D floor, UnityTexture2D ceiling, UnityTexture2D backWall, out float4 baseColor)
{
	float faceIndex;
	float2 uv;

	float3 vAbs = abs(direction);
	float ma;
	if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y)
	{
		faceIndex = direction.z < 0.0 ? 5.0 : 4.0;
		ma = 0.5 / vAbs.z;
		uv = float2(direction.z < 0.0 ? -direction.x : direction.x, -direction.y);
	}
	else if (vAbs.y >= vAbs.x)
	{
		faceIndex = direction.y < 0.0 ? 3.0 : 2.0;
		ma = 0.5 / vAbs.y;
		uv = float2(direction.x, direction.y < 0.0 ? -direction.z : direction.z);
	}
	else
	{
		faceIndex = direction.x < 0.0 ? 1.0 : 0.0;
		ma = 0.5 / vAbs.x;
		uv = float2(direction.x < 0.0 ? direction.z : -direction.z, -direction.y);
	}
	uv = uv * ma + 0.5;
	uv.x = 1 - uv.x;

	baseColor = float4(0, 0, 0, 0);
	if (faceIndex == 0)
	{
		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndicesLRFC.x);
		baseColor = SAMPLE_TEXTURE2D(leftWall, samplerTex, innerUV);
	}
	else if (faceIndex == 1)
	{
		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndicesLRFC.y);
		baseColor = SAMPLE_TEXTURE2D(rightWall, samplerTex, innerUV);
	}
	else if (faceIndex == 2)
	{
		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndicesLRFC.z);
		baseColor = SAMPLE_TEXTURE2D(floor, samplerTex, innerUV);
	}
	else if (faceIndex == 3)
	{
		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndicesLRFC.w);
		baseColor = SAMPLE_TEXTURE2D(ceiling, samplerTex, innerUV);
	}
	else if (faceIndex == 5)
	{
		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndexB);
		baseColor = SAMPLE_TEXTURE2D(backWall, samplerTex, innerUV);
	}
}

void ParalaxInterior_ZPlaneTextureSampler_float(float3 direction, const SamplerState samplerTex, Texture2D zPlaneTexture, out float4 baseColor, out float alpha)
{
	float3 vAbs = abs(direction);
	float ma;
	if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y && direction.z < 0.0)
	{
		float2 uv = float2(direction.z < 0.0 ? -direction.x : direction.x, -direction.y);

		ma = 0.5 / vAbs.z;
		uv = uv * ma + 0.5;
		uv.x = 1 - uv.x;

		baseColor = SAMPLE_TEXTURE2D(zPlaneTexture, samplerTex, uv);
		alpha = baseColor.w;
	}
	else
	{
		baseColor = float4(0, 0, 0, 0);
		alpha = 0;
	}
}
void ParalaxInterior_ZPlaneTextureSampler_Flipbook_float(float3 direction, float2 flipbookSize, float spriteIndex, const SamplerState samplerTex, Texture2D zPlaneTexture, out float4 baseColor, out float alpha)
{
	float3 vAbs = abs(direction);
	float ma;
	if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y && direction.z < 0.0)
	{
		float2 uv = float2(direction.z < 0.0 ? -direction.x : direction.x, -direction.y);

		ma = 0.5 / vAbs.z;
		uv = uv * ma + 0.5;
		uv.x = 1 - uv.x;

		float2 innerUV = FlipbookUV(uv, flipbookSize, spriteIndex);

		baseColor = SAMPLE_TEXTURE2D(zPlaneTexture, samplerTex, innerUV);
		alpha = baseColor.w;
	}
	else
	{
		baseColor = float4(0, 0, 0, 0);
		alpha = 0;
	}
}


void ParalaxBackwall_ScaledSampler_float(float3 direction, const SamplerState samplerTex, float2 uvOffset, float2 uvScale, Texture2D backWall, float4 fallbackColor, out float4 baseColor, out float2 uv)
{
	baseColor = fallbackColor;
	uv = float2(0, 0);

	float3 vAbs = abs(direction);
	if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y && direction.z < 0.0)
	{
		float ma = 0.5 / vAbs.z;
		uv = float2(direction.z < 0.0 ? -direction.x : direction.x, -direction.y);
		uv = uv * ma + 0.5;
		uv.x = 1 - uv.x;

		baseColor = SAMPLE_TEXTURE2D(backWall, samplerTex, (uv + uvOffset) * uvScale);
	}
}