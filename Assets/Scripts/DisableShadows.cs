using UnityEngine;

public class DisableShadows : MonoBehaviour
{
    public Material eyeMat;
    public Material mouthMat;
    private void Start()
    {
        eyeMat.SetShaderPassEnabled("ShadowCaster", false);
        mouthMat.SetShaderPassEnabled("ShadowCaster", false);
    }
}
