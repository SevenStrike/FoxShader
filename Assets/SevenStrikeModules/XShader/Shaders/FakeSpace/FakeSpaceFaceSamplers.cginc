void ParallaxSpaceSampler_float(float3 dir, const SamplerState tex_sample, Texture2D left, Texture2D right, Texture2D floor, Texture2D roof, Texture2D front, out float4 baseColor)
{
    float faceIndex;
    float2 uv;

    float3 vAbs = abs(dir);
    float ma;
    if (vAbs.z >= vAbs.x && vAbs.z >= vAbs.y)
    {
        faceIndex = dir.z < 0.0 ? 5.0 : 4.0;
        ma = 0.5 / vAbs.z;
        uv = float2(dir.z < 0.0 ? -dir.x : dir.x, -dir.y);
    }
    else if (vAbs.y >= vAbs.x)
    {
        faceIndex = dir.y < 0.0 ? 3.0 : 2.0;
        ma = 0.5 / vAbs.y;
        uv = float2(dir.x, dir.y < 0.0 ? -dir.z : dir.z);
    }
    else
    {
        faceIndex = dir.x < 0.0 ? 1.0 : 0.0;
        ma = 0.5 / vAbs.x;
        uv = float2(dir.x < 0.0 ? dir.z : -dir.z, -dir.y);
    }
    uv = uv * ma + 0.5;
    uv.x = 1 - uv.x;

    baseColor = float4(0, 0, 0, 0);
    if (faceIndex == 0)
    {
        baseColor = SAMPLE_TEXTURE2D(left, tex_sample, uv);
    }
    else if (faceIndex == 1)
    {
        baseColor = SAMPLE_TEXTURE2D(right, tex_sample, uv);
    }
    else if (faceIndex == 2)
    {
        baseColor = SAMPLE_TEXTURE2D(floor, tex_sample, uv);
    }
    else if (faceIndex == 3)
    {
        baseColor = SAMPLE_TEXTURE2D(roof, tex_sample, uv);
    }
    else if (faceIndex == 5)
    {
        baseColor = SAMPLE_TEXTURE2D(front, tex_sample, uv);
    }
}