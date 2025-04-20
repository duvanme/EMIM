namespace EMIM.Models;

public class Favorite
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Propiedades de navegación
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}