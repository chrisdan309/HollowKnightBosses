public class IdleState : State
{
    public IdleState(Enemy boss) : base(boss) {}

    public override void Enter() { /* Configuración específica */ }
    public override void Execute() { /* Lógica de ejecución */ }
    public override void Exit() { /* Limpiar al salir */ }
}