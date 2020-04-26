using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;
namespace PipelineDreams {
    public enum TaskPriority
    {
        PLAYER, ENEMY = 100, NPC = 200, SPAWNER = 300, TASKMANAGER = 999
    }
    public class TaskManager : MonoBehaviour {
        PlayerInputBroadcaster PC;
        public float Clock { get; private set; } = 0;


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
        bool IsRunning = false;
        Action OnRunFinish;
        // Start is called before the first frame update
        public void Initialize() {
            PC = FindObjectOfType<PlayerInputBroadcaster>();
        }
        private void Start() {
            AddTime(0);//Alert all event receivers.
        }
        /// <summary>
        /// Warning! AddTime does not lock the main thread; Every function that calls AddTime should be confident that nothing happens after the call and next player input.
        /// </summary>
        /// <param name="time"></param>
        public void AddTime(float time) {
            Clock += time;
            OnClockModified?.Invoke(Clock);
            if (!IsRunning)
                StartCoroutine(RunTasks());
            /*
            else
                AddSequentialTask(new RunTasksTask() { TM = this, StartClock = Clock });
                */
            
        }
        public void AddSequentialTask(IClockTask f) {
            if(f!=null)
            UndoneTasks.Add(f);

        }
        public void RerunTasks() {
            StartCoroutine(RunTasks());
        }

        IEnumerator RunTasks() {
            PC.SetPlayerInputEnabled(PlayerInputFlag.TASKMANAGER, false);
            IsRunning = true;
            for (int i = 0; i < UndoneTasks.Count; i++) {
                if (UndoneTasks[i].StartClock <= Clock) {
                    SequentialTasks.Add(UndoneTasks[i]);
                    UndoneTasks.RemoveAt(i);
                    i--;
                }
            }

            Debug.Log("Clock : " + Clock);
            while (SequentialTasks.Count != 0) {


                SequentialTasks = SequentialTasks.OrderBy((x) => x.Priority).OrderBy((x) => x.StartClock).ToList();
                var t = SequentialTasks[0];
                var task = t.Run();
                if (task != null)
                    yield return task;

                //Debug.Log("Task : " + t.ToString() + ", Priority : " + t.Priority + ", Time : " + t.StartClock + ", Frame : " + Time.frameCount + ", RealTime : " + Time.unscaledTime);
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
            IsRunning = false;
            PC.SetPlayerInputEnabled(PlayerInputFlag.TASKMANAGER, true);
        }
        

    }
    public interface IClockTask {
        TaskPriority Priority { get; }
        float StartClock { get; }
        IEnumerator Run();
    }
    class RunTasksTask : IClockTask
    {
        public TaskManager TM;
        public TaskPriority Priority => TaskPriority.TASKMANAGER;

        public float StartClock { get; set; }

        public IEnumerator Run()
        {
            TM.RerunTasks();
            return null;
        }
    }
}