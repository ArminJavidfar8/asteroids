namespace Services.Data.Abstraction
{
    public interface ISpaceshipData
    {
        string Id { get; }
        int ForwardMotorPower {  get; }
        int RotationPower {  get; }
    }
}