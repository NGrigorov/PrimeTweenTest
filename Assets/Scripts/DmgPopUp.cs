using PrimeTween;
using TMPro;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro damangeInputText;
    [SerializeField] private float duration;

    private Vector3 power = Vector3.up * 0.25f;
    private Vector3 scale;
    private Transform tr;

    private void Awake()
    {
        tr = transform;
        this.scale = tr.localScale;
    }

    public void Initialize(Vector3 location, float amount, Color col)
    {
        damangeInputText.text = amount.ToString("0");
        damangeInputText.color = col;
        tr.localScale = scale;
        tr.position = location;
        Tween.PunchLocalPosition(tr, power, duration).Chain(Tween.Scale(tr, Vector3.zero, duration)).OnComplete(ShowText);
    }

    private void ShowText()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.ObjectType.DamangeNum);
    }
}