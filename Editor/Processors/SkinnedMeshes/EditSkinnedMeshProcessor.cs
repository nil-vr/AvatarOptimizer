using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Anatawa12.AvatarOptimizer.Processors.SkinnedMeshes
{
    internal abstract class EditSkinnedMeshProcessor<TComponent> : IEditSkinnedMeshProcessor
        where TComponent : EditSkinnedMeshComponent
    {
        public abstract int ProcessOrder { get; }
        public virtual IEnumerable<SkinnedMeshRenderer> Dependencies => Array.Empty<SkinnedMeshRenderer>();
        protected TComponent Component { get; }
        public SkinnedMeshRenderer Target { get; }

        EditSkinnedMeshComponent IEditSkinnedMeshProcessor.Component => Component;

        protected EditSkinnedMeshProcessor(TComponent component)
        {
            Component = component;
            Target = component.GetComponent<SkinnedMeshRenderer>();
        }

        public abstract void Process(OptimizerSession session, MeshInfo2 target, MeshInfo2Holder meshInfo2Holder);

        public abstract IMeshInfoComputer GetComputer(IMeshInfoComputer upstream);

        protected bool Equals(EditSkinnedMeshProcessor<TComponent> other) => Component == other.Component;

        public override bool Equals(object obj) =>
            obj != null &&
            (ReferenceEquals(this, obj) || 
             obj.GetType() == this.GetType() && Equals((EditSkinnedMeshProcessor<TComponent>)obj));

        public override int GetHashCode() => Component.GetHashCode();
    }

    internal interface IEditSkinnedMeshProcessor
    {
        int ProcessOrder { get; }
        IEnumerable<SkinnedMeshRenderer> Dependencies { get; }
        SkinnedMeshRenderer Target { get; }
        EditSkinnedMeshComponent Component { get; }
        void Process(OptimizerSession session, MeshInfo2 target, MeshInfo2Holder meshInfo2Holder);

        [NotNull] IMeshInfoComputer GetComputer([NotNull] IMeshInfoComputer upstream);
    }

    internal interface IMeshInfoComputer
    {
        string[] BlendShapes();
        Material[] Materials(bool fast = true);
    }

    internal class AbstractMeshInfoComputer : IMeshInfoComputer
    {
        private readonly IMeshInfoComputer _upstream;

        public AbstractMeshInfoComputer(IMeshInfoComputer upstream)
        {
            _upstream = upstream;
        }

        public virtual string[] BlendShapes() => _upstream?.BlendShapes() ?? Array.Empty<string>();

        public virtual Material[] Materials(bool fast = true) => _upstream?.Materials(fast) ?? Array.Empty<Material>();
    }


    internal class SourceMeshInfoComputer : IMeshInfoComputer
    {
        private readonly SkinnedMeshRenderer _target;

        public SourceMeshInfoComputer(SkinnedMeshRenderer target) => _target = target;


        public static string[] BlendShapes(SkinnedMeshRenderer renderer) => Enumerable
            .Range(0, renderer.sharedMesh.blendShapeCount)
            .Select(i => renderer.sharedMesh.GetBlendShapeName(i))
            .ToArray();

        public static Material[] Materials(SkinnedMeshRenderer renderer) => renderer.sharedMaterials;

        public string[] BlendShapes() => BlendShapes(_target);
        public Material[] Materials(bool fast = true) => Materials(_target);
    }
}
