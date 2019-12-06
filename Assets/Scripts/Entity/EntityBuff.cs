using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buff {
    public BuffData BuData;
    protected TaskManager CM;
    protected Entity Subject;
    public event Action OnDestroy;
    public Buff(Entity subject, BuffData buffData) {
        Subject = subject;
        BuData = buffData;
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TaskManager>();
        CM.OnClockModified += EffectByTime;
        
    }
    protected virtual void EffectByTime(float Time) {

    }
    public virtual void Destroy() {
        OnDestroy?.Invoke();
    }
}
public class BuffTargeted : Buff {
    public BuffTargeted(Entity subject, BuffData buffData) : base(subject, buffData) {
        Subject.GetComponent<EntityHealth>().damageRecieveCoef *= 2f;
    }
    public override void Destroy() {
        base.Destroy();

        Subject.GetComponent<EntityHealth>().damageRecieveCoef /= 2f;
    }
}
public class TimedBuff : Buff {
    public float TimeLeft { get; private set; }
    protected float initialTime;
    public TimedBuff(Entity subject, BuffData buffData):base(subject,buffData) {
        initialTime = CM.Clock;
    }
    protected override void EffectByTime(float Time) {
        TimeLeft = initialTime + BuData.baseDuration - Time;
        if (TimeLeft <0)
            Destroy();
    }
}


public class EntityBuff : MonoBehaviour
{
    BuffDataset DataContainer;
    List<Buff> Buffs;
    EntityManager EM;
    TaskManager CM;
    public event Action<Buff[]> OnRefreshBuffs;
    private void Awake() {
        Buffs = new List<Buff>();
        EM = (EntityManager)FindObjectOfType(typeof(EntityManager));

        CM = (TaskManager)FindObjectOfType(typeof(TaskManager));
        
        CM.OnClockModified += CM_OnClockModified;
        DataContainer = EM.BDataContainer;
    }

    private void CM_OnClockModified(float obj) {
        OnRefreshBuffs?.Invoke(Buffs.ToArray());
    }

    /// <summary>
    /// Adds the Buff corresponding to the name to the inventory.
    /// </summary>
    /// <param name="name"></param>
    public void AddBuff(string name) {
       
        Buff AddedBuff;
        
        BuffData AddedBuffData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
        if (typeof(Buff).Namespace != null)
                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType(typeof(Buff).Namespace + ".Buff" + name),GetComponent<Entity>(), AddedBuffData);
            else {
                
                    AddedBuff = (Buff)Activator.CreateInstance(Type.GetType("Buff" + name), GetComponent<Entity>(), AddedBuffData);
            }
        
        Buffs.Add(AddedBuff);
        OnRefreshBuffs?.Invoke(Buffs.ToArray());
    }
}
