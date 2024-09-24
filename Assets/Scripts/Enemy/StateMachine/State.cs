using System.Collections.Generic;
using UnityEngine;

public class State {
	public List<Action> Actions = new List<Action>();
	public EnemyState StateType;
	

	public State(EnemyState type) {
		StateType = type;
	}

	public void ExecuteStateActions(Enemy enemy) {
		// Numero aleatorio para elegir la acción
		if (Actions.Count == 1) Actions[0].Execute(enemy);
		else{
			int randomAction = Random.Range(0, Actions.Count);
			Actions[randomAction].Execute(enemy);    
		}
	}

	public void OnStateEntry(Enemy enemy){

	}

	public void AddAction(Action action) {
		Actions.Add(action);
	}
	
}