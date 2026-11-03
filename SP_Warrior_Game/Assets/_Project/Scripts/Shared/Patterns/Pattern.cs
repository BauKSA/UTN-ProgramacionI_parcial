abstract class Pattern
{
    private bool _active = false;

    public void Activate() => _active = true;
    public void Deactivate() => _active = false;

    public void Tick()
    {
        if (!_active)
            return;

        if (Wait())
            return;

        Execute();
    }

    protected abstract void Execute();
    protected abstract bool Wait();
}