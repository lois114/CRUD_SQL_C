using Dapper;
using System.Data.SqlClient;

namespace LIB_ANNUAIRE_DAPER;

public class C_BASE 
{
#if DEBUG
    const string Chaine_Connexion = @"Data Source=LAPTOP-I0QJ45LN;Initial Catalog=BDD_A;Integrated Security=True;Connect Timeout=60;Encrypt=False";
#else
   
    const string Chaine_Connexion = @"Server=tcp:serv-lol.database.windows.net,1433;Initial Catalog=BDD-LOL;Persist Security Info=False;User ID=lb;Password=Loisb122;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
#endif
    public C_BASE()
    {

    }
    
    public IEnumerable<C_PERSONNAGES> GET_ALL_PERSONNE()
    {
        using SqlConnection La_Connection = new SqlConnection(Chaine_Connexion);

        La_Connection.Open();

        var Resultat = La_Connection.Query<C_PERSONNAGES>("select * from PERSONNAGES");

        return Resultat;
    }

    public IEnumerable<C_PERSONNAGES> Get_Personne_By_Id(int P_Id)
    {
        using SqlConnection La_Connection = new SqlConnection(Chaine_Connexion);

        La_Connection.Open();

        var Resultat = La_Connection.Query<C_PERSONNAGES>("select * from PERSONNAGES where Id = @NUM", new { NUM = P_Id });

        return Resultat;

    }

    public IEnumerable<C_ROLES> Get_All_Roles()
    {
        using SqlConnection La_Connexion = new SqlConnection(Chaine_Connexion);

        La_Connexion.Open();

        var Resultat = La_Connexion.Query<C_ROLES>("select * from ROLES ");

        return Resultat;
    }

    public IEnumerable<C_PERSONNAGES> Get_Personnage_by_Roles(int P_Id)
    {
        using SqlConnection La_Connexion = new(Chaine_Connexion);

        La_Connexion.Open();

        var Result = La_Connexion.Query<C_PERSONNAGES>("select * from PERSONNAGES where Id_Roles = @Id", new { Id = P_Id });

        return Result;
    }
    public bool Add_Personnage(int P_Id_Roles ,string P_Nom , string P_Image)
    {
        using SqlConnection La_Connection = new SqlConnection(Chaine_Connexion);

        La_Connection.Open();

        var verif = La_Connection.Query<C_PERSONNAGES>("select * from PERSONNAGES where Nom_Personnage = @Nom", new { Nom = P_Nom});


        if (verif.Count() == 0) {
            La_Connection.Execute("INSERT INTO PERSONNAGES (Id_Roles, Nom_Personnage,Image) VALUES (@Id_Roles, @Nom,@Image)", new { Id_Roles = P_Id_Roles, Nom = P_Nom , Image = P_Image });

            return true;
        } else {
            return false;
        }
    }

    public bool Add_Roles(string P_Nom)
    {
        using SqlConnection La_Connexion = new(Chaine_Connexion);

        La_Connexion.Open();

        var verif = La_Connexion.Query<C_ROLES>("select * from ROLES where Nom_Role = @Nom", new { Nom = P_Nom });

        if (verif.Count() == 0) {
            La_Connexion.Query<C_ROLES>("insert into ROLES (Nom_Role) values (@Nom)", new { Nom = P_Nom });

            return true;
        } else {
            return false;
        }
    }

    public bool Delete_Personnage(int P_Id)
    {
        using SqlConnection La_Connexion = new(Chaine_Connexion);

        La_Connexion.Open();

        var verif = La_Connexion.Query<C_PERSONNAGES>("select * from PERSONNAGES where Id_Personnages = @ID", new { ID = P_Id });

        if (verif.Count() != 0) {
            La_Connexion.Query<C_PERSONNAGES>("delete from PERSONNAGES where Id_Personnages = @ID", new { ID = P_Id });
            return true;
        } else {
            return false;
        }

    }

   

    public bool Delete_Roles(int P_Id)
    {
        using SqlConnection La_Connexion = new(Chaine_Connexion);

        La_Connexion.Open();

        var verif = La_Connexion.Query<C_ROLES>("select * from ROLES where Id_Roles = @ID", new { ID = P_Id });

        var verif_1 = Get_Personnage_by_Roles(P_Id);

        if (verif.Count() != 0) {
            if (verif_1 != null) {
                foreach (var Personnage in verif_1) {
                    Delete_Personnage(Personnage.Id_Personnages);
                }
            }

            La_Connexion.Query<C_ROLES>("delete from ROLES where Id_Roles = @ID", new { ID = P_Id });
            return true;
        } else {
            return false;
        }
    }

    public bool Modif_Perso(int P_Id, int P_Id_Role, string P_Nom, string P_URL)
    {
        using SqlConnection La_Connexion = new(Chaine_Connexion);

        La_Connexion.Open();

        var verif = La_Connexion.Query<C_PERSONNAGES>("select * from PERSONNAGES where Id_Personnages = @Id", new { Id = P_Id });

        if (verif.Count() != 0) {
            La_Connexion.Execute("update PERSONNAGES set Id_Roles = @Id_Roles, Nom_Personnage = @Nom_Personnage , Image = @URL where Id_Personnages = @Id_Personnages", new { Id_Personnages = P_Id, Id_Roles = P_Id_Role, Nom_Personnage = P_Nom , URL = P_URL });

            return true;
        } else {
            return false;
        }
    }


    

}
