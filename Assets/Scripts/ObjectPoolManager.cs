using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public enum PoolType
    {
        None = 0,
        DamageNum = 1,
    }

    public enum ObjectType
    {
        None = 0,
        DamangeNum = 1,
    }

    public static ObjectPoolManager Instance;
    public static Dictionary<ObjectType, PooledObjectInfo> ObjectsPoolsDictionary = new Dictionary<ObjectType, PooledObjectInfo>();

    public static PoolType PoolingType;

    private GameObject objectPoolEmptyHolder;
    private static GameObject dmgNumsEmpty;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameStateManager tried to get instanced.");
            return;
        }
        Instance = this;

        SetUpEmpties();
    }

    private void SetUpEmpties()
    {
        objectPoolEmptyHolder = new GameObject("Pooled Objects");

        dmgNumsEmpty = new GameObject("Damage Numbers");

        dmgNumsEmpty.transform.SetParent(objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objecToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None, ObjectType objType = ObjectType.None)
    {
        CheckObjectType(objType);
        PooledObjectInfo pool;
        if (!ObjectsPoolsDictionary.TryGetValue(objType, out pool))
        {
            pool = new PooledObjectInfo();
            ObjectsPoolsDictionary.Add(objType, pool);
        }

        GameObject spawnableObj = pool.inactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);

            spawnableObj = Instantiate(objecToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
                spawnableObj.transform.SetParent(parentObject.transform);
        }

        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.inactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject objecToReturn, ObjectType objType = ObjectType.None)
    {
        CheckObjectType(objType);
        PooledObjectInfo pool;
        if (!ObjectsPoolsDictionary.TryGetValue(objType, out pool))
            Debug.LogError("Pool is null");
        else
        {
            objecToReturn.SetActive(false);
            pool.inactiveObjects.Add(objecToReturn);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        return poolType switch
        {
            PoolType.None => null,
            PoolType.DamageNum => dmgNumsEmpty,
            _ => null,
        };
    }

    private static void CheckObjectType(ObjectType type)
    {
        if (type == ObjectType.None)
            Debug.LogWarning("NONE OBJ TYPE USED");
    }
}

public class PooledObjectInfo
{
    public List<GameObject> inactiveObjects = new();
}