using System.Collections.Generic;
using UnityEngine;

public class State {
	public List<Action> actions = new List<Action>();
	public EnemyState stateType;
	

	public State(EnemyState type) {
		stateType = type;
	}

	public void ExecuteStateActions(Enemy enemy) {
		// Numero aleatorio para elegir la acción
		if (actions.Count == 0) actions[0].Execute(enemy);
		else{
			int randomAction = Random.Range(0, actions.Count);
			actions[randomAction].Execute(enemy);    
		}
	}

	public void OnStateEntry(Enemy enemy){

	}

	public void AddAction(Action action) {
		actions.Add(action);
	}
	
}