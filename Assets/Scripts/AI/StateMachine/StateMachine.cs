using System.Collections.Generic;

public class StateMachine
{
    private int currentState;
    private List<IState> states;

    public StateMachine(List<IState> states)
    {
        this.states = states;
    }

    public void Update()
    {
        states[currentState].Update();
    }

    public void SetState(IState state)
    {
        int index = states.FindIndex( s => s == state );
        currentState = index;
    }
}