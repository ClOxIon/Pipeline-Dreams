using System;

namespace PipelineDreams {
    public interface IIndividualUI<T> {
        event Action OnClick;
        void Clear();
        void AssignHotkeyUI(string keypath);
        void Refresh(T item);
        void SetVisible(bool b);
    }
}