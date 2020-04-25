public class GrowUp : BaseBonus
{
	public delegate void IncreaseBodySizeCallBack();
	public static event IncreaseBodySizeCallBack AddBodyPart;

	public override void PickUp()
	{
		AddBodyPart?.Invoke();
	}
}
