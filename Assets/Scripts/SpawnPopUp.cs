using UnityEngine;

public class SpawnPopUp : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private Vector3 location;
    [SerializeField] private Color col;
    [SerializeField] private int spawnAmount;

    private Vector3 min;
    private Vector3 max;

    private void Awake()
    {
        min = location - (Vector3.one * 5);
        max = location + (Vector3.one * 5);
    }

    private void OnValidate()
    {
        min = location - (Vector3.one * 5);
        max = location + (Vector3.one * 5);
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            DmgPopUpManager.Instance.OnDamageDealt?.Invoke(dmg, pos, col);
        }
    }
}