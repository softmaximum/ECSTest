using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    public class CharacterPlayerInputSystem : ComponentSystem
    {
        private Camera _camera;
        private ComponentGroup _group;

        protected override void OnCreateManager()
        {
            base.OnCreateManager();
            _group = GetComponentGroup(typeof(Character), typeof(CharacterPlayerInput));
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _camera = Camera.main;
        }

        protected override void OnUpdate()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var playerInputs = _group.GetComponentDataArray<CharacterPlayerInput>();
                    for (var i = 0; i < playerInputs.Length; i++)
                    {
                        var input = playerInputs[i];
                        input.Clicked = 1;
                        input.ClickPosition = hit.point;
                        playerInputs[i] = input;
                    }
                }
            }
        }
    }
}