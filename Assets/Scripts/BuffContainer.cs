using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace PipelineDreams
{

    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/Manager/BuffContainer")]
    public class BuffContainer : PDObjectContainer<Buff>
    {
        public override void Init(TaskManager tm, Entity holder)
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