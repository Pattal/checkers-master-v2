using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public interface ICommand
    {
        void Move();
    }

    public static CommandManager Instance { get; private set; }

    private Stack<ICommand> commands = new Stack<ICommand>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCommand(ICommand command)
    {
        command.Move();
        commands.Push(command);
    }

}
