using UnityEngine;

namespace Funzilla
{
	public class FloatingJoystick : Joystick
	{
		protected override void Start()
		{
			base.Start();
			background.gameObject.SetActive(false);
		}

		public override void OnPointerDown(Vector2 touchPosition)
		{
			background.anchoredPosition = ScreenPointToAnchoredPosition(touchPosition);
			background.gameObject.SetActive(true);
			base.OnPointerDown(touchPosition);
		}

		public override void OnPointerUp()
		{
			background.gameObject.SetActive(false);
			base.OnPointerUp();
		}
	}
}