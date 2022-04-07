using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.Input;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Runtime.Game.Ui.Windows.TouchPad
{
	public interface ITouchpadViewController
	{
		void SetActive(bool value);
	}
	
	public class TouchpadViewController : UiController<TouchpadView>, IUiInitializable, ITouchpadViewController, ITickable
	{
		private readonly EcsWorld _world;
		private bool _active;

		private float _startPoint, _endPoint, _dragPoint;
		
		public TouchpadViewController(EcsWorld world)
		{
			_world = world;
			
		}
		public void Initialize()
		{
			View.SetDragAction(OnDragAction);
			View.SetPointerDownAction(OnPointerDownAction);
			View.SetPointerUpAction(OnPointerUpAction);
		}

		public void Tick()
		{
			
		}

		private void OnDragAction(PointerEventData eventData)
		{
			var worldPos = eventData.pointerCurrentRaycast;
			ref var drag = ref _world.NewEntity().Get<PointerDragComponent>();
			drag.Position = worldPos.worldPosition;
		}

		private void OnPointerDownAction(PointerEventData data)
		{
			if(!_active)
				return;
			_world.NewEntity().Get<PointerDownComponent>().Position = data.position;
			//var entity = _world.GetEntity<PlayerComponent>();
			//entity.Get<RemapPointComponent>().Input = data.pointerCurrentRaycast.worldPosition;
			//entity.Get<EventAddComponent<RemapPointComponent>>();
		}

		private void OnPointerUpAction(PointerEventData data)
		{
			if(!_active)
				return;
			_world.NewEntity().Get<PointerUpComponent>().Position = data.position;
		}

		public void SetActive(bool value) => _active = value;
		
	}
}