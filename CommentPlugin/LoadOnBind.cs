using UnityEngine;

namespace CommentMod;

class LoadOnBind : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            AssetBundle bundle = BundleTool.Load("MauricePrime.bundle");
            GameObject prime = SetupResource.SetupGameObject(Instantiate((GameObject)bundle.LoadAsset("Assets/MauricePrime/MauricePrime.prefab")), true);
            
            GameObject player = GameObject.Find("Player");
            if(player != null)
            {
                prime.transform.position = player.transform.position;
            }

            bundle.Unload(false);
        }
    }
}