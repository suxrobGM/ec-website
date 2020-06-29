
namespace EC_Website.Core.Interfaces
{
    /// <summary>
    /// Interface to define set of entity classes
    /// </summary>
    public interface IEntity<TKey> 
    {
        TKey Id { get; set; }
    }
}
