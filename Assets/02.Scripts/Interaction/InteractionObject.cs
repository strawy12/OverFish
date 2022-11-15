using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    private float _delayTime;

    [SerializeField]
    private Sprite _fishIcon;

    [SerializeField]
    private Sprite _baitIcon;

    [SerializeField]
    private Vector3 _interactionUIOffset;

    private InteractionUI _interactionUI;

    private bool _isDelay;
    private bool isFishing;


    private void Awake()
    {
        if (!gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            Debug.LogError($"{gameObject.name}의 Tag가 {Constant.INTERACTION_TAG}으로 설정되지않았습니다.");
        }
    }

    private void Start()
    {
        BindInterationUI();
    }

    private void BindInterationUI()
    {
        _interactionUI = UIManager.Inst.GetInteractionUI();

        isFishing = false;
        _interactionUI.Init();
        _interactionUI.SetIconSprite(_baitIcon);
    }

    public void Update()
    {
        Vector3 pos = transform.position + _interactionUIOffset;
        _interactionUI.SetPos(pos);
    }


    public void TriggerInteraction()
    {
        if (_isDelay) return;

        Debug.Log("Trigger");
        _isDelay = true;

        isFishing = !isFishing;

        if (!isFishing)
        {
            _interactionUI.SetIconSprite(_baitIcon);
        }

        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        float currentDelcayTime = _delayTime;
        while (currentDelcayTime > 0f)
        {
            float fillAmount = currentDelcayTime / _delayTime;
            currentDelcayTime -= Time.deltaTime;

            _interactionUI.SetDelayFill(fillAmount);
            yield return new WaitForEndOfFrame();
        }

        if (isFishing)
        {
            _interactionUI.SetIconSprite(_fishIcon);
        }

        _isDelay = false;
    }
}
