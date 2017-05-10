using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;
using System;

public class StateController : ALComponentSingleton<StateController> {

    [SerializeField]
    private List<State> _stateList;
    public List<State> stateList { get { return _stateList; } }
    private Dictionary<string, State> _stateDictionary;

    [SerializeField]
    private string _nextStateName;

    [SerializeField]
    private string _currentStateName;

    [SerializeField]
    private string _beforeStateName;

    [SerializeField]
    private bool _isExcuteAble;
    public bool isExcuteAble { get { return _isExcuteAble; } set { _isExcuteAble = value; } }

    private State _currentState;
    public State currentState { get { return _currentState; } }

	private void Awake ()
    {
        _stateDictionary = new Dictionary<string, State>();

        for (int i = 0; i < stateList.Count; i++)
        {
            stateList[i].stateController = this;
            _stateDictionary.Add(stateList[i].GetType().Name, stateList[i]);
        }

        if (stateList.Count > 0)
        {
            _currentState = stateList[0];
            _currentStateName = currentState.GetType().Name;
            currentState.OnEnter();
        }
    }

    public void Excute(params object[] datas)
    {
        if (_currentState != null && _isExcuteAble)
        {
            StartCoroutine(_currentState.OnUpdate());
            _isExcuteAble = false;
        }
    }

    public void ChangeState(string name, params object[] datas)
    {
        _beforeStateName = _currentStateName;

        if (currentState != null)
            currentState.OnExit(datas);

        _currentState = _stateDictionary[name];
        currentState.OnEnter(datas);

        _currentStateName = currentState.GetType().Name;
        _isExcuteAble = true;
    }

    public void ChangeBeforeState(params object[] datas)
    {
        if (currentState != null)
            currentState.OnExit(datas);
        _currentState = _stateDictionary[_beforeStateName];
        _currentState.OnEnter(datas);

        _currentStateName = _currentState.GetType().Name;
        _isExcuteAble = true;
    }
}
