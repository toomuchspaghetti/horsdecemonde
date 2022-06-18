using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject
{
	public string label;
	public string determiner;
	public Sprite sprite; 

	public override string ToString() {
		return (determiner != "" ? (determiner + " ") : "") + label;
	}
}