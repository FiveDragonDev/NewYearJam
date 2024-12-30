public static class PlayerInput
{
    public static Controller Input => _input ?? CreateInput();

    private static Controller _input;

    private static Controller CreateInput()
    {
        _input = new();
        _input.Base.Enable();

        return _input;
    }
}
