

namespace BO;

[Serializable]
public class BlTheEntityDoesNotExist : Exception
{
    public BlTheEntityDoesNotExist(string message) : base(message) { }
    public BlTheEntityDoesNotExist(string message, Exception innerException) : base(message, innerException) { }

}

[Serializable]
public class BlTheCodeAlreadyExists : Exception
{
    public BlTheCodeAlreadyExists(string message) : base(message) { }
    public BlTheCodeAlreadyExists(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable]
public class BlNotEnoughInStock : Exception
{
    public BlNotEnoughInStock(string message) : base(message) { }
    public BlNotEnoughInStock(string message, Exception innerException) : base(message, innerException) { }
}
