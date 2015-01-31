﻿using System;
namespace SLua {
	public partial class LuaObject {
		public static void BindUnityUI(IntPtr l) {
			Lua_UnityEngine_EventSystems_EventHandle.reg(l);
			Lua_UnityEngine_EventSystems_UIBehaviour.reg(l);
			Lua_UnityEngine_EventSystems_EventSystem.reg(l);
			Lua_UnityEngine_EventSystems_EventTrigger.reg(l);
			Lua_UnityEngine_EventSystems_EventTrigger_TriggerEvent.reg(l);
			Lua_UnityEngine_EventSystems_EventTrigger_Entry.reg(l);
			Lua_UnityEngine_EventSystems_EventTriggerType.reg(l);
			Lua_UnityEngine_EventSystems_ExecuteEvents.reg(l);
			Lua_UnityEngine_EventSystems_MoveDirection.reg(l);
			Lua_UnityEngine_EventSystems_RaycastResult.reg(l);
			Lua_UnityEngine_EventSystems_BaseEventData.reg(l);
			Lua_UnityEngine_EventSystems_AxisEventData.reg(l);
			Lua_UnityEngine_EventSystems_PointerEventData.reg(l);
			Lua_UnityEngine_EventSystems_PointerEventData_InputButton.reg(l);
			Lua_UnityEngine_EventSystems_PointerEventData_FramePressState.reg(l);
			Lua_UnityEngine_EventSystems_BaseInputModule.reg(l);
			Lua_UnityEngine_EventSystems_PointerInputModule.reg(l);
			Lua_UnityEngine_EventSystems_PointerInputModule_MouseButtonEventData.reg(l);
			Lua_UnityEngine_EventSystems_StandaloneInputModule.reg(l);
			Lua_UnityEngine_EventSystems_TouchInputModule.reg(l);
			Lua_UnityEngine_EventSystems_BaseRaycaster.reg(l);
			Lua_UnityEngine_EventSystems_PhysicsRaycaster.reg(l);
			Lua_UnityEngine_EventSystems_Physics2DRaycaster.reg(l);
			Lua_UnityEngine_UI_AnimationTriggers.reg(l);
			Lua_UnityEngine_UI_Selectable.reg(l);
			Lua_UnityEngine_UI_Button.reg(l);
			Lua_UnityEngine_UI_Button_ButtonClickedEvent.reg(l);
			Lua_UnityEngine_UI_CanvasUpdate.reg(l);
			Lua_UnityEngine_UI_CanvasUpdateRegistry.reg(l);
			Lua_UnityEngine_UI_ColorBlock.reg(l);
			Lua_UnityEngine_UI_FontData.reg(l);
			Lua_UnityEngine_UI_FontUpdateTracker.reg(l);
			Lua_UnityEngine_UI_Graphic.reg(l);
			Lua_UnityEngine_UI_GraphicRaycaster.reg(l);
			Lua_UnityEngine_UI_GraphicRaycaster_BlockingObjects.reg(l);
			Lua_UnityEngine_UI_GraphicRebuildTracker.reg(l);
			Lua_UnityEngine_UI_GraphicRegistry.reg(l);
			Lua_UnityEngine_UI_MaskableGraphic.reg(l);
			Lua_UnityEngine_UI_Image.reg(l);
			Lua_UnityEngine_UI_Image_Type.reg(l);
			Lua_UnityEngine_UI_Image_FillMethod.reg(l);
			Lua_UnityEngine_UI_Image_OriginHorizontal.reg(l);
			Lua_UnityEngine_UI_Image_OriginVertical.reg(l);
			Lua_UnityEngine_UI_Image_Origin90.reg(l);
			Lua_UnityEngine_UI_Image_Origin180.reg(l);
			Lua_UnityEngine_UI_Image_Origin360.reg(l);
			Lua_UnityEngine_UI_InputField.reg(l);
			Lua_UnityEngine_UI_InputField_ContentType.reg(l);
			Lua_UnityEngine_UI_InputField_InputType.reg(l);
			Lua_UnityEngine_UI_InputField_CharacterValidation.reg(l);
			Lua_UnityEngine_UI_InputField_LineType.reg(l);
			Lua_UnityEngine_UI_InputField_SubmitEvent.reg(l);
			Lua_UnityEngine_UI_InputField_OnChangeEvent.reg(l);
			Lua_UnityEngine_UI_Navigation.reg(l);
			Lua_UnityEngine_UI_Navigation_Mode.reg(l);
			Lua_UnityEngine_UI_RawImage.reg(l);
			Lua_UnityEngine_UI_Scrollbar.reg(l);
			Lua_UnityEngine_UI_Scrollbar_Direction.reg(l);
			Lua_UnityEngine_UI_Scrollbar_ScrollEvent.reg(l);
			Lua_UnityEngine_UI_ScrollRect.reg(l);
			Lua_UnityEngine_UI_ScrollRect_MovementType.reg(l);
			Lua_UnityEngine_UI_ScrollRect_ScrollRectEvent.reg(l);
			Lua_UnityEngine_UI_Selectable_Transition.reg(l);
			Lua_UnityEngine_UI_Slider.reg(l);
			Lua_UnityEngine_UI_Slider_Direction.reg(l);
			Lua_UnityEngine_UI_Slider_SliderEvent.reg(l);
			Lua_UnityEngine_UI_SpriteState.reg(l);
			Lua_UnityEngine_UI_StencilMaterial.reg(l);
			Lua_UnityEngine_UI_Text.reg(l);
			Lua_UnityEngine_UI_Toggle.reg(l);
			Lua_UnityEngine_UI_Toggle_ToggleTransition.reg(l);
			Lua_UnityEngine_UI_Toggle_ToggleEvent.reg(l);
			Lua_UnityEngine_UI_ToggleGroup.reg(l);
			Lua_UnityEngine_UI_AspectRatioFitter.reg(l);
			Lua_UnityEngine_UI_AspectRatioFitter_AspectMode.reg(l);
			Lua_UnityEngine_UI_CanvasScaler.reg(l);
			Lua_UnityEngine_UI_CanvasScaler_ScaleMode.reg(l);
			Lua_UnityEngine_UI_CanvasScaler_ScreenMatchMode.reg(l);
			Lua_UnityEngine_UI_CanvasScaler_Unit.reg(l);
			Lua_UnityEngine_UI_ContentSizeFitter.reg(l);
			Lua_UnityEngine_UI_ContentSizeFitter_FitMode.reg(l);
			Lua_UnityEngine_UI_LayoutGroup.reg(l);
			Lua_UnityEngine_UI_GridLayoutGroup.reg(l);
			Lua_UnityEngine_UI_GridLayoutGroup_Corner.reg(l);
			Lua_UnityEngine_UI_GridLayoutGroup_Axis.reg(l);
			Lua_UnityEngine_UI_GridLayoutGroup_Constraint.reg(l);
			Lua_UnityEngine_UI_HorizontalOrVerticalLayoutGroup.reg(l);
			Lua_UnityEngine_UI_HorizontalLayoutGroup.reg(l);
			Lua_UnityEngine_UI_LayoutElement.reg(l);
			Lua_UnityEngine_UI_LayoutRebuilder.reg(l);
			Lua_UnityEngine_UI_LayoutUtility.reg(l);
			Lua_UnityEngine_UI_VerticalLayoutGroup.reg(l);
			Lua_UnityEngine_UI_Mask.reg(l);
			Lua_UnityEngine_UI_BaseVertexEffect.reg(l);
			Lua_UnityEngine_UI_Shadow.reg(l);
			Lua_UnityEngine_UI_Outline.reg(l);
			Lua_UnityEngine_UI_PositionAsUV1.reg(l);
		}
	}
}
