public interface IHealth
{
    /// <summary>
    /// Xử lí máu khi nhận sát thương
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(float damage);
    /// <summary>
    /// Khởi tạo máu khi bắt đầu game hoặc xuất hiện
    /// </summary>
    void InitHeal();
}
