namespace GodEnums
{
	public enum AnimationMixOptions
	{
		Additive,
		BlendingWithMixingTransform,
	}

	public enum MouseButtons
	{
		Left = 0,
		Right,
		Middle
	}

	public enum FadeStates
	{
		In,
		Out,
		FadingOut,
		FadingIn
	}

	public enum GameSetupLayouts
	{
		One = 0,
		Vertical,
		Horizontal,
		Up,
		UpEquals,
		Down,
		DownEquals,
		Left,
		LeftEquals,
		Right,
		RightEquals,
		Four
	}

	public enum ControlAssignments
	{
		KeyboardAndMouse = 0,
		Gamepad1,
		Gamepad2,
		Gamepad3,
		Gamepad4,
		Awaiting,
		Unassigned
	}

	public enum GamePads
	{
		None,
		P1,
		P2,
		P3,
		P4,
	}

	public enum ControllerTypes
	{
		KeyboardMouse,
		Gamepad,
		Touchscreen,
		Remote,
		Cpu,
		None
	}

	public enum GamePadButtons
	{
		X,
		Y,
		A,
		B,
		LB,
		LS,
		RB,
		RS,
		BACK,
		START,
		LTD,
		RTD
	}

	public enum GamePadAxes
	{
		LH,
		LV,
		RH,
		RV,
		DH,
		DV,
		TA
	}

	public enum AnalogTriggers
	{
		Left,
		Right
	}

	public enum ButtonStates
	{
		Idle,
		Down,
		Pressed,
		Up
	}

	public enum PlayModeOptions
	{
		Once = 0,
		Looped,
		ClampForever,
	}
}






