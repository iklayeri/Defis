using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Defis.Dto;

public partial class DtoCategorie
{
    public int Id { get; set; }

    public string? Code { get; set; }
[Required]
    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public DateTime? DateCreation { get; set; }

    //public DateTime? DateModification { get; set; }

    public bool? EstActif { get; set; }

   // public string? ModifierPar { get; set; }

    public string? Userid { get; set; }

   // public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
