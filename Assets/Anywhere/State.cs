using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

    private StateController _stateController;
    public StateController stateController { get { return _stateController; } set { _stateController = value; } }

    public abstract void OnEnter(params object[] datas);    
    public abstract IEnumerator OnUpdate(params object[] datas);  
    public abstract void OnExit(params object[] datas);    
}
