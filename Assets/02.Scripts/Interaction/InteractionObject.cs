using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField]
    private float _delayTime;

    [SerializeField]
    private Sprite _interactionIcon;

    [SerializeField]
    private Vector3 _interactionUIOffset;

    private InteractionUI _interactionUI;

    private bool _isDelay;


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

        _interactionUI.Init();
        _interactionUI.SetIconSprite(_interactionIcon);
    }

    public void LateUpdate()
    {
        Vector3 pos = transform.position + _interactionUIOffset;
        _interactionUI.SetPos(pos);
    }

    public virtual void EnterInteraction() { }

    public abstract void TriggerInteraction();

    public virtual void ExitInteraction() { }

    private IEnumerator StartDelay()
    {
        _isDelay = true;

        float currentDelcayTime = _delayTime;
        while (currentDelcayTime > 0f)
        {
            float fillAmount = currentDelcayTime / _delayTime;
            currentDelcayTime -= Time.deltaTime;

            _interactionUI.SetDelayFill(fillAmount);
            yield return new WaitForEndOfFrame();
        }

        _isDelay = false;
    }
}
