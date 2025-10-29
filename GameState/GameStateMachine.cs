using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Common;
using UnityEngine;

namespace GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;

        public GameStateMachine(params IGameState[] states)
        {
            _states = states.ToDictionary(s => s.GetType(), s => s);
        }

        public void ChangeState<T>() where T : IGameState
        {
            if (_currentState != null)
                 _currentState.Exit();

            _currentState = _states[typeof(T)];
            Dbg.Log($"Change game state to: {_currentState.GetType().Name}", Color.darkOrange);
             _currentState.Enter();
        }
    }
}