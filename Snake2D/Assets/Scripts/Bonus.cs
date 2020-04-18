using UnityEngine;


[CreateAssetMenu(fileName = "New Bonus", menuName = "Bonuses")]
public class Bonus : ScriptableObject
{
	public Sprite BonusIcon;
	public string BonusName;
	public int Value;
}
