using System.Collections;
using System.Security.AccessControl;

namespace PipelineDreams.Instruction
{
    public partial class Container
    {
        public class InstCheckTask : IClockTask
        {
            public Container cont;
            public TaskPriority Priority => TaskPriority.INSTCHECK;
            public float StartClock { get; set; }

            public IEnumerator Run()
            {
                var n = cont.objs.Count;
                for (int i = 0; i < n; i++) {
                    if (cont.objs[cont.CurrentEntryPoint].ReadCommand()) {
                        yield return cont.objs[cont.CurrentEntryPoint].Operation(StartClock);
                        break;
                    }
                    cont.CurrentEntryPoint++;
                    if (cont.CurrentEntryPoint == n) 
                        cont.CurrentEntryPoint -= n;

                }
            }
        }

    }
}