using UnityEngine.Events;

public class MonoMgr : SingletonAutoMono<MonoMgr>
{
    private event UnityAction startEvent; 
    private event UnityAction fixedEvent;
    private event UnityAction updateEvent;
    private event UnityAction lateEvent;

    public void AddStartListener(UnityAction action) //添加Start事件监听
    {
        startEvent += action;
    }
    public void RemoveStartListener(UnityAction action) //移除Start事件监听
    {
        startEvent -= action;
    }

    private void Start() //调用Start事件
    {
        startEvent?.Invoke();
    }
    public void AddFixedUpdatListener(UnityAction action) //添加FixedUpdate事件监听
    {
        fixedEvent += action;
    }
    public void RemoveFixedUpdatListener(UnityAction action) //移除FixedUpdate事件监听
    {
        fixedEvent -= action;
    }

    private void FixedUpdate() //调用FixedUpdate事件
    {
        fixedEvent?.Invoke();
    }
    public void AddUpdatListener(UnityAction action) ///添加Update事件监听
    {
        updateEvent += action;
    }
    public void RemoveUpdatListener(UnityAction action) //移除Update事件监听
    {
        updateEvent -= action;
    }
    private void Update() //调用Update事件
    {
        updateEvent?.Invoke();
    }
    public void AddLateUpdateListener(UnityAction action) //添加LateUpdate事件监听
    {
        lateEvent += action;
    }
    public void RemoveLateUpdateListener(UnityAction action) //移除LateUpdate事件监听
    {
        lateEvent -= action;
    }
    private void LateUpdate() //调用LateUpdate事件
    {
        lateEvent?.Invoke();
    }

}
