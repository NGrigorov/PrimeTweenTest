using System;
using UnityEngine;

public class DmgPopUpManager : MonoBehaviour
{
    [SerializeField] private DmgPopUp prefab;

    public static DmgPopUpManager Instance;
    public Action<float, Vector3, Color> OnDamageDealt;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameStateManager tried to get instanced.");
            return;
        }
        Instance = this;
        Instance.OnDamageDealt += Instance.GenerateDamagePopUp;
    }

    private void OnDestroy()
    {
        Instance.OnDamageDealt -= Instance.GenerateDamagePopUp;
    }

    private void GenerateDamagePopUp(float amount, Vector3 location, Color col)
    {
        if (amount <= 0.001)
            return;

        DmgPopUp dmgPopUp = ObjectPoolManager.SpawnObject(prefab.gameObject, Vector3.zero, Quaternion.identity, ObjectPoolManager.PoolType.DamageNum, ObjectPoolManager.ObjectType.DamangeNum).GetComponent<DmgPopUp>();
        dmgPopUp.Initialize(location, amount, col);
    }

}