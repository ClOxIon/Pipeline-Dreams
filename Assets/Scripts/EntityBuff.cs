using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buff {
    public BuffData BuData;
    protected ClockManager CM;
    protected Entity Subject;
    public event Action OnDestroy;
    public Buff(Entity subject, BuffData buffData) {
        Subject = subject;
        BuData = buffData;
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ClockManager>();
        CM.OnClockModified += EffectByTime;
        
    }
    protected virtual void EffectByTime(float Time) {

    }
    public virtual void Destroy() {
        OnDestroy?.Invoke();
    }
}
public class TimedBuff : Buff {
    float timeleft;
    protected float initialTime;
    public TimedBuff(Entity subject, BuffData buffData):base(subject,buffData) {
        initialTime = CM.Clock;
    }
    protected override void EffectByTime(float Time) {
        if (initialTime + BuData.baseDuration <= Time)
            Destroy();
    }
}


public class EntityBuff : MonoBehaviour
{
    [SerializeField] BuffDataset DataContainer;
    List<Buff> Buffs;
    EntityManager EM;
    private void Awake() {
        EM = (EntityManager)FindObjectOfType(typeof(EntityManager));
        DataContainer = EM.BDataContainer;
    }
    /// <summary>
    /// Adds the Buff corresponding to the name to the inventory.
    /// </summary>
    /// <param name="name"></param>
    public void AddBuff(string name) {
       
        Buff AddedBuff;
        BuffData AddedBuffData;
        
            if (typeof(Buff).Namespace != null)
                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType(typeof(Buff).Namespace + ".Buff" + name));
            else {
                
                    AddedBuff = (Buff)Activator.CreateInstance(Type.GetType("Buff" + name));
            }
        AddedBuffData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
        Buffs.Add(AddedBuff);
    }
}
