public class SpeedUp : BaseBonus
{
	public delegate void IncreaseSpeedCallBack();
	public static event IncreaseSpeedCallBack SpeedUpEvent;

	public override void PickUp()
	{
		SpeedUpEvent?.Invoke();
	}
}
