using UnityEngine;

namespace CommentMod;

public static class BundleTool
{
    public static void Load(string fileName) {
        AssetBundle bundle = AssetBundle.LoadFromMemory(EmbeddedAccess.AccessFile(fileName));
        GameObject prime = Object.Instantiate((GameObject)bundle.LoadAsset("Assets/Maurice/MauricePrime.prefab"));

        prime.transform.parent = null;

        bundle.Unload(false);
    }
}