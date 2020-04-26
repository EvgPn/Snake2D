using System.Collections;
using UnityEngine;

public class SlowTime : BaseBonus
{
	public override void PickUp()
	{
		Time.timeScale = 0.5f;
		StartCoroutine("IncreaseTimeScale");
	}

	private IEnumerator IncreaseTimeScale()
	{
		yield return new WaitForSeconds(2);
		Time.timeScale = 1f;
	}
}
