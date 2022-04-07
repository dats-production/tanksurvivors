using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace CustomSelectables
{
    [AddComponentMenu("UI/Custom/Custom Button")]
    public class CustomButton : CustomSelectable, IPointerClickHandler, ISubmitHandler
    {
//----------------------ON-CLICK-------------------------
        
        [FormerlySerializedAs("onClick")] [SerializeField]
        private SimpleEvent m_OnClick = new SimpleEvent();
        
        public SimpleEvent onClick
        {
            get => m_OnClick;
            set => m_OnClick = value;
        }
        
        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }
        
        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }
        
        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }

//-------------------------------------------------------
    }
}
