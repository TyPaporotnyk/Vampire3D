using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GPUInstansingEnabler : MonoBehaviour
{
    void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}