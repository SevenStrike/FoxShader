using UnityEditor;
using UnityEngine;

public class FakeInteriorShaderGUI : ShaderGUI
{
    public const string KEYWORD_LIGHTMAPS = "FAKEINTERIOR_LIGHTMAPS_ON";
    public const string KEYWORD_ADD_SKYBOX = "FAKEINTERIOR_ADD_SKYBOX_ON";
    public const string KEYWORD_TILED = "FAKEINTERIOR_TILED_ON";
    public const string KEYWORD_FLIPBOOK = "FAKEINTERIOR_FLIPPBOOK_TEXTS_ON";
    public const string KEYWORD_FLIPBOOK_INTERNAL_PLANE = "FAKEINTERIOR_FLIPPBOOK_TEXTS_INTERNALPLANE_ON";

    public const string APHHA_TEST = "_ALPHATEST_ON";
    public const string ALPHA_CUTOFF = "_AlphaCutoffEnable";

    // Default keywords
    const string SUPPORT_DECALS = "_SupportDecals";
    const string RECEIVES_SSR = "_ReceivesSSR";

    // Foldout prefs
    const string FOLDOUT_PREFIX = "Knackelibang_";
    const string OUTWALL_FOLDOUT = FOLDOUT_PREFIX + "OuterWall";
    const string TEXTURE_FOLDOUT = FOLDOUT_PREFIX + "Texture";
    const string LIGHTMAPS_FOLDOUT = FOLDOUT_PREFIX + "Lightmaps";
    const string INTERIORPLANE_FOLDOUT = FOLDOUT_PREFIX + "InteriorPlane";
    const string GLASSPROPS_FOLDOUT = FOLDOUT_PREFIX + "GlassProperties";
    const string TILING_FOLDOUT = FOLDOUT_PREFIX + "Tiling";
    const string ADDREDL_FOLDOUT = FOLDOUT_PREFIX + "AddReflections";

    const string _AssetTypeHDRP = "HDRenderPipelineAsset";
    bool IsHDRP()
    {
        return UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.GetType().Name.Contains(_AssetTypeHDRP);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        Material targetMat = materialEditor.target as Material;

        EditorGUILayout.LabelField("Default properties", EditorStyles.boldLabel);

        var supportDecals = FindProperty(SUPPORT_DECALS, properties, false);
        var receivedSSR = FindProperty(RECEIVES_SSR, properties, false);

        if (supportDecals != null)
            materialEditor.ShaderProperty(supportDecals, "Receive Decals");

        if (receivedSSR != null)
            materialEditor.ShaderProperty(receivedSSR, "Receive SSR/SSGI");

        targetMat.enableInstancing = EditorGUILayout.Toggle("Enable Instancing", targetMat.enableInstancing);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Variations", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUI.BeginChangeCheck();

        var lightmaps = targetMat.IsKeywordEnabled(KEYWORD_LIGHTMAPS);
        var alphaCutoff = targetMat.IsKeywordEnabled(APHHA_TEST);
        var addSkybox = targetMat.IsKeywordEnabled(KEYWORD_ADD_SKYBOX);
        var tiled = targetMat.IsKeywordEnabled(KEYWORD_TILED);
        var flipbook = targetMat.IsKeywordEnabled(KEYWORD_FLIPBOOK);
        var flipbookIntPlane = targetMat.IsKeywordEnabled(KEYWORD_FLIPBOOK_INTERNAL_PLANE);

        if (IsHDRP() == false)
        {
            EditorGUILayout.LabelField("With URP, Alpha Cutoff is 'Alpha Clip' in the Shader Graph options", EditorStyles.boldLabel);
            EditorGUILayout.Space();
        }

        lightmaps = EditorGUILayout.Toggle("Lightmaps", lightmaps);

        if (IsHDRP())
        {
            alphaCutoff = EditorGUILayout.Toggle("Alpha Cutoff", alphaCutoff);
        }

        addSkybox = EditorGUILayout.Toggle("Additional Reflections", addSkybox);
        tiled = EditorGUILayout.Toggle("Tiled", tiled);
        flipbook = EditorGUILayout.Toggle("Flipbook Textures", flipbook);
        flipbookIntPlane = EditorGUILayout.Toggle("Flipbook Internal Plane Texture", flipbookIntPlane);
        if (EditorGUI.EndChangeCheck())
        {
            if (lightmaps)
                targetMat.EnableKeyword(KEYWORD_LIGHTMAPS);
            else
                targetMat.DisableKeyword(KEYWORD_LIGHTMAPS);

            if (alphaCutoff)
            {
                targetMat.EnableKeyword(APHHA_TEST);
                FindProperty(ALPHA_CUTOFF, properties).floatValue = 1;
            }
            else
            {
                targetMat.DisableKeyword(APHHA_TEST);
                FindProperty(ALPHA_CUTOFF, properties).floatValue = 0;
            }

            if (addSkybox)
                targetMat.EnableKeyword(KEYWORD_ADD_SKYBOX);
            else
                targetMat.DisableKeyword(KEYWORD_ADD_SKYBOX);

            if (tiled)
                targetMat.EnableKeyword(KEYWORD_TILED);
            else
                targetMat.DisableKeyword(KEYWORD_TILED);

            if (flipbook)
                targetMat.EnableKeyword(KEYWORD_FLIPBOOK);
            else
                targetMat.DisableKeyword(KEYWORD_FLIPBOOK);

            if (flipbookIntPlane)
                targetMat.EnableKeyword(KEYWORD_FLIPBOOK_INTERNAL_PLANE);
            else
                targetMat.DisableKeyword(KEYWORD_FLIPBOOK_INTERNAL_PLANE);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Base Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        materialEditor.RangeProperty(FindProperty("_Depth", properties), "Depth");
        materialEditor.RangeProperty(FindProperty("_InnerScale", properties), "Inner Scale");
        materialEditor.RangeProperty(FindProperty("_Metallic", properties), "Metallic");
        materialEditor.RangeProperty(FindProperty("_Smoothness", properties), "Smoothness");
        materialEditor.ColorProperty(FindProperty("_EmissiveColor", properties), "Emissive Color");
        materialEditor.FloatProperty(FindProperty("_EmissiveStrength", properties), "Emissive Strength");

        if (alphaCutoff)
            materialEditor.VectorProperty(FindProperty("_CutoffEdges", properties), "Cutoff Edges");

        var noOuterWall = FindProperty("_InnerScale", properties).floatValue == 1f;
        EditorGUI.BeginDisabledGroup(noOuterWall);
        EditorGUI.indentLevel++;

        var outerWallFold = EditorPrefs.GetBool(OUTWALL_FOLDOUT);
        var texturesFold = EditorPrefs.GetBool(TEXTURE_FOLDOUT);
        var lightmapsFold = EditorPrefs.GetBool(LIGHTMAPS_FOLDOUT);
        var interiorPlaneFold = EditorPrefs.GetBool(INTERIORPLANE_FOLDOUT);
        var glassPropertiesFold = EditorPrefs.GetBool(GLASSPROPS_FOLDOUT);
        var tilingFold = EditorPrefs.GetBool(TILING_FOLDOUT);
        var addReflectionsFold = EditorPrefs.GetBool(ADDREDL_FOLDOUT);

        if (noOuterWall)
            outerWallFold = EditorGUILayout.BeginFoldoutHeaderGroup(outerWallFold, "Outer Wall (Inner scale < 1)");
        else
            outerWallFold = EditorGUILayout.BeginFoldoutHeaderGroup(outerWallFold, "Outer Wall");

        if (outerWallFold)
        {
            materialEditor.TextureProperty(FindProperty("_OuterWallTexture", properties), "Outer Wall Texture");
            materialEditor.TextureScaleOffsetProperty(FindProperty("_OuterWallTexture", properties));
            materialEditor.ColorProperty(FindProperty("_OuterWallColor", properties), "Outer Wall Color");
            materialEditor.RangeProperty(FindProperty("_OuterWallMetallic", properties), "Metallic");
            materialEditor.RangeProperty(FindProperty("_OuterWallSmoothness", properties), "Smoothness");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
        EditorGUI.EndDisabledGroup();

        texturesFold = EditorGUILayout.BeginFoldoutHeaderGroup(texturesFold, "Textures");
        EditorPrefs.SetBool("", texturesFold);
        if (texturesFold)
        {
            materialEditor.TextureProperty(FindProperty("_SideWallsTexture", properties), "Side Walls");
            materialEditor.TextureProperty(FindProperty("_BackWallTexture", properties), "Back Wall");
            materialEditor.TextureProperty(FindProperty("_FloorTexture", properties), "Floor");
            materialEditor.TextureProperty(FindProperty("_CeilingTexture", properties), "Ceiling");

            if (targetMat.IsKeywordEnabled(KEYWORD_FLIPBOOK))
                materialEditor.VectorProperty(FindProperty("_TextureFlipbookSize", properties), "Flipbook Size");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        if (targetMat.IsKeywordEnabled(KEYWORD_LIGHTMAPS))
        {
            lightmapsFold = EditorGUILayout.BeginFoldoutHeaderGroup(lightmapsFold, "Lightmaps");
            if (lightmapsFold)
            {
                materialEditor.TextureProperty(FindProperty("_SideWallsLightmap", properties), "Side Walls Lightmap");
                materialEditor.TextureProperty(FindProperty("_BackWallLightmap", properties), "Back Wall Lightmap");
                materialEditor.TextureProperty(FindProperty("_FloorLightmap", properties), "Floor Lightmap");
                materialEditor.TextureProperty(FindProperty("_CeilingLightmap", properties), "Ceiling Lightmap");
                materialEditor.ColorProperty(FindProperty("_LightmapColor", properties), "Lightmap Color");
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        interiorPlaneFold = EditorGUILayout.BeginFoldoutHeaderGroup(interiorPlaneFold, "Interior Plane");
        if (interiorPlaneFold)
        {
            materialEditor.TextureProperty(FindProperty("_InteriorPlane", properties), "Texture");

            if (targetMat.IsKeywordEnabled(KEYWORD_LIGHTMAPS))
                materialEditor.TextureProperty(FindProperty("_InteriorPlaneLightmap", properties), "Lightmap");

            if (targetMat.IsKeywordEnabled(KEYWORD_FLIPBOOK_INTERNAL_PLANE))
                materialEditor.VectorProperty(FindProperty("_InteriorPlaneFlipbookSize", properties), "Flipbook Size");

            materialEditor.RangeProperty(FindProperty("_InteriorPlaneDepth", properties), "Plane Depth");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        glassPropertiesFold = EditorGUILayout.BeginFoldoutHeaderGroup(glassPropertiesFold, "Glass");
        if (glassPropertiesFold)
        {
            materialEditor.TextureProperty(FindProperty("_GlassTexture", properties), "Texture");
            materialEditor.TextureScaleOffsetProperty(FindProperty("_GlassTexture", properties));
            materialEditor.ColorProperty(FindProperty("_GlassColor", properties), "Color");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        if (targetMat.IsKeywordEnabled(KEYWORD_TILED))
        {
            tilingFold = EditorGUILayout.BeginFoldoutHeaderGroup(tilingFold, "Tiling & Tiled Lighting");
            if (tilingFold)
            {
                materialEditor.VectorProperty(FindProperty("_TilingOffset", properties), "Tiling & Offset");
                materialEditor.ColorProperty(FindProperty("_LightmapColorA", properties), "Lightmap Color A");
                materialEditor.ColorProperty(FindProperty("_LightmapColorB", properties), "Lightmap Color B");
                materialEditor.FloatProperty(FindProperty("_HueSpread", properties), "Hue Spread");
                materialEditor.FloatProperty(FindProperty("_LightSteps", properties), "Light Steps");
                materialEditor.VectorProperty(FindProperty("_LightRemap", properties), "Light Range");
                materialEditor.FloatProperty(FindProperty("_MinLightValue", properties), "Min Light Value");
                materialEditor.FloatProperty(FindProperty("_Seed", properties), "Seed");
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        if (targetMat.IsKeywordEnabled(KEYWORD_ADD_SKYBOX))
        {
            addReflectionsFold = EditorGUILayout.BeginFoldoutHeaderGroup(addReflectionsFold, "Additional Reflections");
            if (addReflectionsFold)
            {
                materialEditor.TextureProperty(FindProperty("_Skybox", properties), "Skybox");
                materialEditor.RangeProperty(FindProperty("_BaseReflection", properties), "Base Reflection");
                materialEditor.RangeProperty(FindProperty("_EdgeReflection", properties), "Edge Reflection");
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }


        EditorPrefs.SetBool(OUTWALL_FOLDOUT, outerWallFold);
        EditorPrefs.SetBool(TEXTURE_FOLDOUT, texturesFold);
        EditorPrefs.SetBool(LIGHTMAPS_FOLDOUT, lightmapsFold);
        EditorPrefs.SetBool(INTERIORPLANE_FOLDOUT, interiorPlaneFold);
        EditorPrefs.SetBool(GLASSPROPS_FOLDOUT, glassPropertiesFold);
        EditorPrefs.SetBool(TILING_FOLDOUT, tilingFold);
        EditorPrefs.SetBool(ADDREDL_FOLDOUT, addReflectionsFold);

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(targetMat);
    }
}
