using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ClockManager : MonoBehaviour
{

    PlayerController PC;
    public float Clock { get; private set; } = 0;
    float AccumulatedClock = 0;


    /// <summary>
    /// These tasks are done sequentially.
    /// </summary>
    List<IClockTask> SequentialTasks = new List<IClockTask>();
    /// <summary>
    /// This is a list of undone tasks.
    /// </summary>
    List<IClockTask> UndoneTasks = new List<IClockTask>();
    public event Action OnTaskEnd;
    
    public event Action<float> OnClockModified;
    bool[] TaskEndFlag;
    // Start is called before the first frame update
    private void Awake() {
        PC = GetComponent<PlayerController>();
    }
    public void AddTime(float time) {
        Clock += time;
        
        OnClockModified?.Invoke(Clock);
        StartCoroutine(RunTasks());
    }
    public void AddSequentialTask(IClockTask f) {
        UndoneTasks.Add(f);

    }
    /*
    public void RemoveSequentialTask(Func<IEnumerator> f) {
        SequentialTasks.Remove(f);
    }
    */


    /*
    public void RemoveImmediateTask(Func<IEnumerator> f) {
        ImmediateTasks.Remove(f);
    }
    */

    IEnumerator RunTasks() {
        PC.DisableInput(PlayerInputFlag.CLOCKMANAGER);
        /*
        TaskEndFlag = new bool[ImmediateTasks.Count];
        var Tasks = new List<IClockTask>(ImmediateTasks);
        Tasks.AddRange(SequentialTasks);
        Tasks.OrderBy((x) => x.StartClock);
        foreach (var x in Tasks) {
            x.RunImmediately();
        }
        */
        //SequentialTasks.OrderBy((x) => x.StartClock);
        /*
        for (int i=0;i<ImmediateTasks.Count;i++) {

            StartCoroutine(RunTaskAndRaiseFlag(ImmediateTasks[i], i));
            
        }
        yield return new WaitUntil(() => { var result = true; foreach (var b in TaskEndFlag) { result &= b; } return result; });
        */
        for (int i = 0; i < UndoneTasks.Count; i++) {
            if (UndoneTasks[i].StartClock <= Clock) {
                SequentialTasks.Add(UndoneTasks[i]);
                UndoneTasks.RemoveAt(i);
                i--;
            }
        }

        Debug.Log("Clock : " + Clock);
        while (SequentialTasks.Count != 0) {
            
            
            SequentialTasks = SequentialTasks.OrderBy((x)=>x.Priority).OrderBy((x) => x.StartClock).ToList();
            var t = SequentialTasks[0];
            if (t.HasIteration)
                yield return t.Run();
            else
                t.Run();
            Debug.Log("Task : " + t.ToString() + ", Priority : " + t.Priority + ", Time : " + t.StartClock + ", Frame : " + Time.frameCount + ", RealTime : " + Time.unscaledTime);
            SequentialTasks.Remove(t);
            OnTaskEnd?.Invoke();

            for (int i = 0; i < UndoneTasks.Count; i++) {
                if (UndoneTasks[i].StartClock <= Clock) {
                    SequentialTasks.Add(UndoneTasks[i]);
                    UndoneTasks.RemoveAt(i);
                    i--;
                }
            }
            

        }

        //ImmediateTasks.Clear();
        SequentialTasks.Clear();
        PC.EnableInput(PlayerInputFlag.CLOCKMANAGER);
    }
    IEnumerator RunTaskAndRaiseFlag(IClockTask f, int i) {
        yield return f.Run();
        TaskEndFlag[i] = true;
    }

}
public interface IClockTask {
    int Priority { get; }
    float StartClock { get; }
    bool HasIteration { get; }
    IEnumerator Run();
    
}
