namespace PerelesoqTest.Gameplay.Gadgets.Connectors
{
    public class Bridge : ConnectorBase
    {
        protected override void ChangeOutputCurrent()
        {
            outputPort.Current = inputPort.Inputs[0].Current;
            base.ChangeOutputCurrent();
        }
    }
}