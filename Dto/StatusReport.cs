using System;
namespace Defis.Dto
{
	public class StatusReport
	{
        public bool Valide { get; set; } = false;
        public string? Message { get; set; }
        public Object? Resultat { get; set; }
        public Object? Reponse { get; set; }
    }
}

