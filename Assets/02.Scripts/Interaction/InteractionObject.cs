using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField]
    private float _delayTime;

    [SerializeField]
    protected Sprite _interactionIcon;

    [SerializeField]
    protected Vector3 _interactionUIOffset;

    [SerializeField]
    protected bool notUsedIconUI;

    protected InteractionUI _interactionUI = null;
    public InteractionUI interactionUI => _interactionUI;

    protected bool _isDelay;


    protected virtual void Awake()
    {
        if (!gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            Debug.LogError($"{gameObject.name}의 Tag가 {Constant.INTERACTION_TAG}으로 설정되지않았습니다.");
        }
    }

    protected virtual void Start()
    {
        GameManager.Inst.GameStart += () => _interactionUI?.SetDelayFill(0f);
        BindInterationUI();
    }

    protected virtual void BindInterationUI()
    {
        _interactionUI = UIManager.Inst.GetInteractionUI();

        _interactionUI.Init();
        _interactionUI.SetIconSprite(_interactionIcon);
    }

    public void LateUpdate()
    {
        if (notUsedIconUI)
        {
            if(_interactionUI.gameObject.activeSelf)
            {
                _interactionUI.gameObject.SetActive(false);
            }
            return;
        }

        Vector3 pos = transform.position + _interactionUIOffset;
        _interactionUI.SetPos(pos);
    }

    public virtual void EnterInteraction() { }

    public abstract void TriggerInteraction();

    public virtual void ExitInteraction() { }

    protected void StartDelay(float delay)
    {
        _delayTime = delay;
        StopAllCoroutines();
        StartCoroutine(StartDelay());
    }
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

        EndDelay();
    }

    protected virtual void EndDelay() { }
}
