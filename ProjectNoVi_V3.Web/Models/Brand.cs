using Microsoft.Build.Framework;

namespace ProjectNoVi_V3.Models;

public class Brand
{
    public Brand() 
    {
        MerkName= string.Empty;
    }
    public int Id { get; set; }
    [Required]
    public string MerkName { get; set; }
}

