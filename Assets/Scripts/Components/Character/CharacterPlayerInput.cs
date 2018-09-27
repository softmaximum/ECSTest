using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    public struct CharacterPlayerInput : IComponentData
    {
        public int Clicked;
        public float3 ClickPosition;
    }
}