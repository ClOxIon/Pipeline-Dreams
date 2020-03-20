using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace PipelineDreams.Buff
{

    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/Manager/BuffContainer")]
    public class Container : PDObjectContainer<Buff>
    {
        public override void Init(TaskManager tm, Entity.Entity holder)
        {
            base.Init(tm, holder);
            tm.OnClockModified += TM_OnClockModified;
        }
        private void TM_OnClockModified(float obj)
        {
            InvokeUIRefresh();
        }

    }
}