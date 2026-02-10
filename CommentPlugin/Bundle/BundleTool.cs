using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CommentMod;

public static class BundleTool
{
    public static AssetBundle Load(string fileName) {
        AssetBundle bundle = AssetBundle.LoadFromMemory(EmbeddedAccess.AccessFile(fileName));
        return bundle;
    }
}