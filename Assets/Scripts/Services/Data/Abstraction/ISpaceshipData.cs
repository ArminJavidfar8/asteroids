namespace Services.Data.Abstraction
{
    public interface ISpaceshipData
    {
        string Id { get; }
        int MaxHealth { get; }
        int ForwardMotorPower {  get; }
        int RotationPower {  get; }
        int KillingScore {  get; }
    }
}