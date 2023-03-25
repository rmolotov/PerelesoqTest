namespace PerelesoqTest.Infrastructure.States.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload levelStaticData);
    }
}