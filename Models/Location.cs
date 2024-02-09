using System;
using System.Collections.Generic;

namespace Defis.Models;

public partial class Location
{
    public Guid Id { get; set; }

    public int? FilmId { get; set; }

    public Guid? UtilisateurId { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool? EstActif { get; set; }

    public string? ModifierPar { get; set; }

    public string? Userid { get; set; }

    public virtual Film? Film { get; set; }

    public virtual Utilisateur? Utilisateur { get; set; }
}
