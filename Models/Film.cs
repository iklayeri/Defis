using System;
using System.Collections.Generic;

namespace Defis.Models;

public partial class Film
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Titre { get; set; }

    public string? Description { get; set; }

    public int? Duree { get; set; }

    public bool? EstDisponible { get; set; }

    public int? CategorieId { get; set; }

    public int? AuteurId { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool? EstActif { get; set; }

    public string? ModifierPar { get; set; }

    public string? Userid { get; set; }

    public virtual Auteur? Auteur { get; set; }

    public virtual Category? Categorie { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
