namespace TheBox.Domain.Shared.Entities;

public abstract class GuidValueObject
{
    protected GuidValueObject(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Value cannot be default", nameof(value));
        }

        this.Value = value;
    }

    public Guid Value { get; }

    // Override equality methods
    public override bool Equals(object? obj)
    {
        if (obj is GuidValueObject valueObject)
        {
            return this.Value == valueObject.Value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.Value.GetHashCode();
    }
}