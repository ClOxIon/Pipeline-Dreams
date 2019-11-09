using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public enum Command {
left = 0, up, right,down, space
}
public class PipelineController : MonoBehaviour
{
    int Length = 3;
    [SerializeField] GameObject Container;
    [SerializeField] GameObject SpacerContainer;
    [SerializeField] GameObject Spacer;
    [SerializeField]List<GameObject> CommandPrefabs;
    Queue<GameObject> CommandObjects = new Queue<GameObject>();
    List<GameObject> Spacers = new List<GameObject>();
    Queue<Command> Commands = new Queue<Command>();
    public event Action<Command> OnPop;
    // Start is called before the first frame update
    private void Awake() {
       
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>().OnCommandKeyPressed += PushCommand;
        LengthChange(6);
    }
    void Start()
    {

    }
    public void LengthChange(int length) {
        Length = length;
        GetComponent<RectTransform>().sizeDelta = new Vector2(length*100, 100);
        foreach (var o in Spacers)
            Destroy(o);
        Spacers.Clear();
        for(int i=0;i<length;i++)
        Spacers.Add(Instantiate(Spacer, SpacerContainer.transform, false));
        RefreshSpacers();
    }
    void PushCommand(Command i) {

        Commands.Enqueue(i);
        CommandObjects.Enqueue(Instantiate(CommandPrefabs[(int)i], Container.transform,false));
        if (Commands.Count > Length) {
            var v = Commands.Dequeue();
            OnPop?.Invoke(v);
            Destroy(CommandObjects.Dequeue());
            
        }
        RefreshSpacers();
    }
    public void DeleteCommandFromFront(int number) {
        while (number > 0) {
            Commands.Dequeue();
            Destroy(CommandObjects.Dequeue());
            number--;
        }
        RefreshSpacers();
    }
    public void DeleteCommandFromBack(int number) {
        
        var CommandsList = Commands.ToList();
        var c = Commands.Count;
        CommandsList.RemoveRange(c - number, number);
        Commands = new Queue<Command>(CommandsList);
        var CommandObjectsList = CommandObjects.ToList(); 
        for (int i = c - number;i<c;i++)
            Destroy(CommandObjectsList[i]);
        CommandObjectsList.RemoveRange(c - number, number);
        CommandObjects = new Queue<GameObject>(CommandObjectsList);
        
        RefreshSpacers();
    }
    public void DeleteCommandAt(int index, int number) {
        //Debug.Log("DeleteCommandAt : "+index+","+ number);
        var CommandsList = Commands.ToList();
        var c = Commands.Count;
        CommandsList.RemoveRange(index, number);
        Commands = new Queue<Command>(CommandsList);
        var CommandObjectsList = CommandObjects.ToList();
        for (int i = index; i < index+number; i++)
            Destroy(CommandObjectsList[i]);
        CommandObjectsList.RemoveRange(index, number);
        CommandObjects = new Queue<GameObject>(CommandObjectsList);

        RefreshSpacers();
    }
    public void Flush() {
        while (Commands.Count>0) {
            Commands.Dequeue();
            Destroy(CommandObjects.Dequeue());
            
        }
        RefreshSpacers();
    }
    void RefreshSpacers() {
    /*
        int i = 0;
        for (; i < Commands.Count; i++)
            Spacers[i].SetActive(false);
        for (; i < length; i++)
            Spacers[i].SetActive(true);
            */
    }
    public Command[] CurrentPipeline() {
        return Commands.ToArray();
        
       
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
