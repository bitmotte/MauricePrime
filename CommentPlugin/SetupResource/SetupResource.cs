using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CommentMod;

public static class SetupResource
{
    public static GameObject SetupGameObject(GameObject gameObject, bool vertexLighting)
    {
        gameObject.transform.parent = null;

        // Load material with "ULTRAKILL/Master" shader . . . and grab its shader
        Shader Master = Addressables.LoadAssetAsync<Material>("Assets/Materials/Environment/Metal/Pattern 1/Metal Pattern 1 8.mat").WaitForCompletion().shader;
        
        foreach (MeshRenderer still in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            foreach (Material mat in still.materials)
            {
                mat.shader = Master;
                if(vertexLighting)
                {
                    List<string> keywords = [.. mat.shaderKeywords];
                    keywords.Add("VERTEX_LIGHTING");
                    mat.shaderKeywords = [.. keywords];
                }
            }
        }

        foreach (SkinnedMeshRenderer skinned in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (Material mat in skinned.materials)
            {
                mat.shader = Master;
                if(vertexLighting)
                {
                    List<string> keywords = [.. mat.shaderKeywords];
                    keywords.Add("VERTEX_LIGHTING");
                    mat.shaderKeywords = [.. keywords];
                }
            }
        }

        return gameObject;
    }
}