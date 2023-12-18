using LIB_ANNUAIRE_DAPER;

namespace TEST_LIB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            C_BASE La_Base = new();

            //La_Base.Add_Personne("AATROX");

            //Console.WriteLine();

            //var Les_Personne = La_Base.GET_ALL_PERSONNE();

            //var Les_Personne = La_Base.GET_ALL_PERSONNE();
            var Les_Roles = La_Base.Get_All_Roles();

           var Les_Personne = La_Base.Get_Personnage_by_Roles(2);


            foreach (var Une_Personne in Les_Personne) {
                Console.WriteLine($"{Une_Personne.Nom_Personnage}| {Une_Personne.Id_Personnages}|");
            }

            //foreach (var Un_Roles in Les_Roles) {
            //    Console.WriteLine($"{Un_Roles.Nom_Role}");
            //}
        }
    }
}
