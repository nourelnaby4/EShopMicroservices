namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string messsage) : base(messsage) 
    {
    }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
