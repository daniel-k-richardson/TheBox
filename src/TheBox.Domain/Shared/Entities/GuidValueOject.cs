namespace TheBox.Domain.Shared.Entities;

#pragma warning disable SA1649 // File name should match first type name
public abstract class GuidValueObject
{
    protected GuidValueObject(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Value cannot be default", nameof(value));
        }

        Value = value;
    }

    public Guid Value { get; }

    public override bool Equals(object? obj)
    {
        if (obj is GuidValueObject valueObject)
        {
            return Value == valueObject.Value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    // Override equality methods
}
