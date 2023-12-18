namespace LIB_ANNUAIRE_DAPER

{
    public class C_PERSONNAGES 
    {
        public int Id_Personnages { get; set; }

        public string Nom_Personnage { get; set; }

        public int Id_Roles { get; set; }

        public string Image { get; set; }

        public override string ToString()
        {
            return $" {Id_Roles,3} {Nom_Personnage,3}";
        }
    }
}
