using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required int Userid { get; set; }

    [ForeignKey("Userid")]
    public required Users Users { get; set; }
    public required string Task { get; set; }
    public required string Status { get; set; }

}