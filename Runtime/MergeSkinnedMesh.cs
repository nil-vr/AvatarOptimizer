using Anatawa12.AvatarOptimizer.ErrorReporting;
using CustomLocalization4EditorExtension;
using UnityEngine;

namespace Anatawa12.AvatarOptimizer
{
    [AddComponentMenu("Avatar Optimizer/Merge Skinned Mesh")]
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [DisallowMultipleComponent]
    internal class MergeSkinnedMesh : EditSkinnedMeshComponent, IStaticValidated
    {
        [CL4EELocalized("MergeSkinnedMesh:prop:renderers")]
        public PrefabSafeSet.SkinnedMeshRendererSet renderersSet;
        [CL4EELocalized("MergeSkinnedMesh:prop:staticRenderers")]
        public PrefabSafeSet.MeshRendererSet staticRenderersSet;
        public PrefabSafeSet.MaterialSet doNotMergeMaterials;

        // common between v0 and v1
        [CL4EELocalized("MergeSkinnedMesh:prop:removeEmptyRendererObject")]
        [ToggleLeft]
        public bool removeEmptyRendererObject = true;

        public MergeSkinnedMesh()
        {
            renderersSet = new PrefabSafeSet.SkinnedMeshRendererSet(this);
            staticRenderersSet = new PrefabSafeSet.MeshRendererSet(this);
            doNotMergeMaterials = new PrefabSafeSet.MaterialSet(this);
        }
    }
}
