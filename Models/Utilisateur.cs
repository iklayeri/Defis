using System;
using System.Collections.Generic;

namespace Defis.Models;

public partial class Utilisateur
{
    public Guid Id { get; set; }

    public string? Nom { get; set; }

    public string? Prenoms { get; set; }

    public string? AdresseGeographique { get; set; }

    public string? Telephone { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool? EstActif { get; set; }

    public string? ModifierPar { get; set; }

    public string? Userid { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
