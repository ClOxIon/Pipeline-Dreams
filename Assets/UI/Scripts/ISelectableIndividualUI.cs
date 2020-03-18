namespace PipelineDreams {
    public interface ISelectableIndividualUI<T> : IIndividualUI<T> {
        void SetSelection(bool b);
    }
}