namespace Management.Abstraction
{
    public interface IDamageable
    {
        float MaxHealth {  get; }
        float Health { get; }
        bool IsAlive { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns true if its dead</returns>
        bool TakeDamage(float damage);
        void Die();
    }
}