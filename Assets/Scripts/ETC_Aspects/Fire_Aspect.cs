using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SFGA.Test
{
    public readonly partial struct Fire_Aspect : IAspect
    {
        public readonly Entity entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<FireTag> _fireTag;
        private readonly RefRW<Fire_Component> _fireComponent;

        public float3 position
        {
            get => _fireComponent.ValueRO.position;
            set => _fireComponent.ValueRW.position = value;
        }

        public Rotation rotation
        {
            get => _fireComponent.ValueRO.rotation;
            set => _fireComponent.ValueRW.rotation = value;
        }
    }
}

